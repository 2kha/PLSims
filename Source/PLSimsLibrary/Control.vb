Namespace PLSims

    Public Module Control


        'This moudule contians the functionality to manage
        'instructions fetching, sequence of execution, and
        'program control

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'Constant Declarations

        'System Time Constant
        Public Const SYS_MAXTIME = 999999999999L

        'System Null Constant
        Public Const SYS_NULL = Nothing

        'Variable Declaration

        'System Variable
        Public SYS_SCOPE As Integer         'System Scope
        Public SYS_NX_ADDR As Long          'Next Address
        Public SYS_EXECUTABLE As Boolean    'System Executable Flag
        Public SYS_RETURN As Boolean        'System Return Flag

        Public SYS_ARGS() As Object         'System Arguments
        Public SYS_ARG_INDEX As Integer     'System Argument Index


        'System Debugging
        Public LineNumber As Integer
        Public Current_Instruction As String
        Public SYS_ERR_CODE As Integer

        'System Block
        Public SYS_PROGRAM_BLOCK = "Program Block"
        Public SYS_ENDPROGRAM_BLOCK = "End Program Block"
        Public SYS_PROCEDURE_BLOCK = "Procedure Block"
        Public SYS_ENDPROCEDURE_BLOCK = "End Procedure Block"
        Public SYS_FUNCTION_BLOCK = "Function Block"
        Public SYS_ENDFUNCTION_BLOCK = "End Function Block"
        Public SYS_IF_BLOCK = "If Block"
        Public SYS_THEN_BLOCK = "Then Block"
        Public SYS_ELSE_BLOCK = "Else Block"
        Public SYS_ENDIF_BLOCK = "End If Block"
        Public SYS_WHILE_BLOCK = "While Block"
        Public SYS_ENDWHILE_BLOCK = "End While Block"
        Public SYS_REPEAT_BLOCK = "Repeat Block"
        Public SYS_UNTIL_BLOCK = "Until Block"
        Public SYS_CODE_BLOCK = "Code Block"

        'System Attribute Type
        Public SYS_ATTR_ID = "ID"
        Public SYS_ATTR_TEXT = "Text"
        Public SYS_ATTR_TYPE = "Type"
        Public SYS_ATTR_SCOPE = "Scope"
        Public SYS_ATTR_PARENT = "Parent"
        Public SYS_ATTR_CHILDREN = "Children"

        'System Nodes
        Public SYS_NODE_PROGRAM = "Program"
        Public SYS_NODE = "Node"

        Private Sub LOAD_INSTR(ByVal IN_STR As String)

            Dim ch As String
            Dim Index As Long

            Dim IR As String

            PR_INDEX = 0

            DF_INDEX = 0
            VR_INDEX = 0

            LV_START = 0
            LV_END = 0
            LV_PTR = 0

            For Index = 1 To Len(IN_STR)

                ch = Mid(IN_STR, Index, 1)

                If ((ch = ChrW(WS_VT)) Or (ch = ChrW(WS_FF)) Or _
                (ch = ChrW(WS_CR)) Or (ch = ChrW(WS_LF))) Then

                    If (REMOVE_WSPC(IR) <> vbNullString) Then

                        PR_INDEX = PR_INDEX + 1
                        ReDim Preserve PRO(PR_INDEX)
                        PRO(PR_INDEX) = IR

                    End If

                    IR = vbNullString
                Else
                    IR = IR + ch
                End If

            Next Index

            If (IR <> vbNullString) Then

                PR_INDEX = PR_INDEX + 1
                ReDim Preserve PRO(PR_INDEX)
                PRO(PR_INDEX) = IR

            End If

        End Sub

        Private Sub INSTR_CYCLE()

            Dim IR As String        'Instruction Register
            Dim PAR As Long         'Program Address Register
            Dim INST As Integer

            Dim TimeEllapsed As Long

            TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp()

            TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp() - TimeEllapsed


            PAR = 1
            SYS_SCOPE = -2
            SYS_ERR_CODE = 0

            LOAD_ERROR_MESSAGES()


            While (PAR <= PR_INDEX And SYS_ERR_CODE = 0 And TimeEllapsed < SYS_MAXTIME)

                TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp()

                LineNumber = PAR

                IR = PRO(PAR)

                Current_Instruction = IR

                INST = INTERPRET(IR)

                EXECUTE(INST, PAR)

                If (SYS_NX_ADDR = 0) Then
                    PAR = PAR + 1
                Else
                    PAR = SYS_NX_ADDR
                    SYS_NX_ADDR = 0
                End If

                TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp() - TimeEllapsed

            End While

            If (SYS_SCOPE = -2) Then
                SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_PROGRAM_STRUCT
            ElseIf (TimeEllapsed > SYS_MAXTIME) Then
                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_STACK_OVERFLOW
            End If

        End Sub

        'To Build Code Structured in Tree Format
        Private Function CODETREE() As TObjects.TObject

            Dim CODEDOM As New TObjects.TObject

            Dim Parent As New TObjects.TObject

            Dim IR As String
            Dim PAR As Long
            Dim INST As Integer

            Dim Children As Integer
            Dim ParentName As String

            Children = 0
            ParentName = String.Empty

            SYS_ERR_CODE = 0
            SYS_SCOPE = -2

            PAR = 1

            While (PAR <= PR_INDEX)

                IR = PRO(PAR)

                Current_Instruction = IR

                INST = INTERPRET(IR)

                Select Case INST

                    Case INSTR_PROG

                        CODEDOM(SYS_NODE_PROGRAM)(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(SYS_NODE_PROGRAM)(SYS_ATTR_TYPE).Data = SYS_PROGRAM_BLOCK
                        CODEDOM(SYS_NODE_PROGRAM)(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        CODEDOM(SYS_NODE_PROGRAM)(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = CODEDOM(SYS_NODE_PROGRAM)(SYS_ATTR_CHILDREN)
                        ParentName = SYS_NODE_PROGRAM

                        SYS_SCOPE = -1

                    Case INSTR_END_PROG

                        SYS_SCOPE = -2

                    Case INSTR_GLOBAL_DEF

                        Children = Parent.TObjects.Count

                        If (Children = 0) Then

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Else

                            Parent.First()(SYS_ATTR_TEXT).Data = Parent.First()(SYS_ATTR_TEXT).Data & ";" & IR

                        End If

                    Case INSTR_LOCAL_DEF

                        Children = Parent.TObjects.Count

                        If (Children = 0) Then

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Else

                            Parent.First()(SYS_ATTR_TEXT).Data = Parent.First()(SYS_ATTR_TEXT).Data & ";" & IR

                        End If

                    Case INSTR_PROC_DEF

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_PROCEDURE_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1


                    Case INSTR_FUNC_DEF

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_FUNCTION_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_END_PROC_CALL

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_END_PROC

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_END_FUNC

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_FUNC_RTR

                        Children = Parent.TObjects.Count

                        If (Children = 0) Then

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Else

                            Parent.First()(SYS_ATTR_TEXT).Data = Parent.First()(SYS_ATTR_TEXT).Data & ";" & IR

                        End If

                    Case INSTR_PROC_MAIN

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_PROCEDURE_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_IF

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_IF_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_THEN

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_THEN_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_ELSE

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_ELSE_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_END_IF

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_WHILE

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_WHILE_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_END_WHILE

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_REPEAT

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_REPEAT_BLOCK
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2
                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                        Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Parent = Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN)

                        ParentName = SYS_NODE & PAR

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_UNTIL

                        ParentName = Parent.Parent(SYS_ATTR_PARENT).Data

                        Parent = Parent.Parent.Parent

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_EXP

                        Children = Parent.TObjects.Count

                        If (Children = 0 Or Children > 1) Then

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TEXT).Data = IR.Replace(Chr(34), Chr(92) & Chr(34)).Trim()
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_PARENT).Data = ParentName

                            Parent((SYS_NODE & PAR).ToString())(SYS_ATTR_CHILDREN).Set(New TObjects.TObject())

                        Else

                            Parent.First()(SYS_ATTR_TEXT).Data = Parent.First()(SYS_ATTR_TEXT).Data & ";" & IR.Replace(Chr(34), Chr(92) & Chr(34)).Trim()

                        End If

                    Case Else

                End Select

                PAR = PAR + 1

            End While

            Return CODEDOM

        End Function

        Private Function CODESTRUCTURE() As TObjects.TObject

            Dim CODEDOM As New TObjects.TObject

            Dim Parent As New TObjects.TObject


            Dim IR As String
            Dim PAR As Long
            Dim INST As Integer

            Dim Index As Integer
            Dim PreIndex As Integer


            Dim Children As Integer
            Dim ParentName As String

            Children = 0
            ParentName = String.Empty

            SYS_SCOPE = -2

            PAR = 1

            Index = 0
            PreIndex = 0

         
            While (PAR <= PR_INDEX)

                IR = PRO(PAR)

                Current_Instruction = IR

                INST = INTERPRET(IR)

                Select Case INST

                    Case INSTR_PROG

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_PROGRAM_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = -1

                    Case INSTR_END_PROG

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ENDPROGRAM_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = -2

                    Case INSTR_GLOBAL_DEF

                        If (CODEDOM(PreIndex.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK) Then

                            CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data = CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data & ";" & IR.Trim()
                        Else

                            CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                            CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                            CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            PreIndex = Index

                            Index = Index + 1

                        End If

                    Case INSTR_LOCAL_DEF

                        If (CODEDOM(PreIndex.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK) Then

                            CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data = CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data & ";" & IR.Trim()
                        Else

                            CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                            CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                            CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            PreIndex = Index

                            Index = Index + 1

                        End If

                    Case INSTR_PROC_DEF

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_PROCEDURE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1


                    Case INSTR_FUNC_DEF

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_FUNCTION_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_END_PROC_CALL, INSTR_END_PROC

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ENDPROCEDURE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_END_FUNC, INSTR_END_FUNC_CALL

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ENDFUNCTION_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_FUNC_RTR

                        If (CODEDOM(PreIndex.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK) Then

                            CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data = CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data & ";" & IR.Trim()
                        Else

                            CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                            CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                            CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            PreIndex = Index

                            Index = Index + 1

                        End If

                    Case INSTR_PROC_MAIN

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_PROCEDURE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_IF

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_IF_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_THEN

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_THEN_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1


                    Case INSTR_ELSE

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ELSE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1



                    Case INSTR_END_IF
                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ENDIF_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_WHILE

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_WHILE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_END_WHILE

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_ENDWHILE_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_REPEAT

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_REPEAT_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE + 1

                    Case INSTR_UNTIL

                        CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                        CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Trim()
                        CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_UNTIL_BLOCK
                        CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                        PreIndex = Index

                        Index = Index + 1

                        SYS_SCOPE = SYS_SCOPE - 1

                    Case INSTR_EXP


                        If (CODEDOM(PreIndex.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK) Then

                            CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data = CODEDOM(PreIndex.ToString())(SYS_ATTR_TEXT).Data & ";" & IR.Replace(Chr(34), Chr(92) & Chr(34)).Trim()
                        Else

                            CODEDOM(Index.ToString())(SYS_ATTR_ID).Data = Index
                            CODEDOM(Index.ToString())(SYS_ATTR_TEXT).Data = IR.Replace(Chr(34), Chr(92) & Chr(34)).Trim()
                            CODEDOM(Index.ToString())(SYS_ATTR_TYPE).Data = SYS_CODE_BLOCK
                            CODEDOM(Index.ToString())(SYS_ATTR_SCOPE).Data = SYS_SCOPE + 2

                            PreIndex = Index

                            Index = Index + 1

                        End If


                    Case Else

                End Select

                PAR = PAR + 1

            End While

            Return CODEDOM

        End Function

        Public Function CODE_DOM(ByVal IN_STR As String) As TObjects.TObject

            Dim CODEDOM As TObjects.TObject

            CODEDOM = Nothing

            If (Not String.IsNullOrEmpty(IN_STR)) Then
                LOAD_INSTR(IN_STR)

                CODEDOM = CODESTRUCTURE()
            End If

            Clear()

            Return CODEDOM

        End Function


        Public Function CODE_TREE(ByVal IN_STR As String) As TObjects.TObject

            Dim CODEDOM As TObjects.TObject

            CODEDOM = Nothing

            If (Not String.IsNullOrEmpty(IN_STR)) Then
                LOAD_INSTR(IN_STR)

                CODEDOM = CODETREE()
            End If

            Clear()

            Return CODEDOM

        End Function

        Public Sub RUN(ByVal IN_STR As String)

            Evaluation.OUTPUT_STRING = String.Empty

            Dim TimeEllapsed As Long

            TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp()

            Clear()

            If (Not String.IsNullOrEmpty(IN_STR)) Then

                LOAD_INSTR(IN_STR)
                INSTR_CYCLE()

            End If

            TimeEllapsed = System.Diagnostics.Stopwatch.GetTimestamp() - TimeEllapsed

            Evaluation.OUTPUT_ELAPSEDTIME = TimeSpan.FromTicks(TimeEllapsed).TotalMilliseconds / 1000.0


        End Sub

        Public Sub Clear()

            If (Not IsNothing(PRO)) Then
                Array.Clear(PRO, 0, PRO.Length)
            End If

            If (Not IsNothing(LVR)) Then
                Array.Clear(LVR, 0, LVR.Length)
            End If

            If (Not IsNothing(VAR)) Then
                VAR.Clear()
            End If

            If (Not IsNothing(DEF)) Then
                DEF.Clear()
            End If

            If (Not IsNothing(POX)) Then
                Array.Clear(POX, 0, POX.Length)
            End If

            If (Not IsNothing(OPR)) Then
                Array.Clear(OPR, 0, OPR.Length)
            End If

            GC.Collect(1)
        End Sub

        Public Function REMOVE_WSPC(ByVal Strs As String) As String

            Dim L As Integer
            Dim ch As String = String.Empty

            Dim NwStr As String = String.Empty

            For L = 1 To Len(Strs)

                ch = Mid$(Strs, L, 1)

                If ((ch <> ChrW(WS_SP)) And (ch <> ChrW(WS_TB))) Then
                    NwStr = NwStr + ch
                End If

            Next

            Return NwStr

        End Function

        Public Function REMOVE_RT(ByVal Strs As String) As String

            Dim L As Integer
            Dim ch As String = String.Empty

            Dim NwStr As String = String.Empty

            For L = 1 To Len(Strs)

                ch = Mid$(Strs, L, 1)

                If ((ch = ChrW(WS_VT)) Or (ch = ChrW(WS_FF)) Or _
                    (ch = ChrW(WS_CR)) Or (ch = ChrW(WS_LF))) Then
                Else

                    NwStr = NwStr + ch
                End If

            Next

            Return NwStr

        End Function

        Public Function REMOVE_IVC(ByVal Strs As String) As String

            Dim L As Integer
            Dim ch As String

            Dim NwStr As String = String.Empty

            For L = 2 To (Len(Strs) - 1)
                ch = Mid$(Strs, L, 1)
                NwStr = NwStr + ch
            Next

            Return NwStr

        End Function

        Public Function GET_ESCAPE(ByVal CODE As String) As String

            If (IsEquStr(CODE, KW_ENTER)) Then
                Return vbCrLf
            ElseIf (IsEquStr(CODE, KW_SPACE)) Then
                Return ChrW(WS_SP)
            ElseIf (IsEquStr(CODE, KW_TAB)) Then
                Return vbTab
            ElseIf (IsEquStr(CODE, KW_BR)) Then
                Return "<br/>"
            ElseIf (IsEquStr(CODE, KW_NPSP)) Then
                Return "&nbsp;"

            End If

            Return Nothing

        End Function

    End Module

End Namespace
