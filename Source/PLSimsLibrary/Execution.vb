Namespace PLSims

    Public Module Execution


        'This module contain the functionality to control
        'execution of instructions

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        'TODO: Need to refactor
        Public Sub EXECUTE(ByVal INSTR_TYPE As Integer, ByVal CUR_ADDR As Long)

            Dim NX_ADDR As Long

            Dim N As Integer
            Dim Index As Integer

            Dim Result As Object
            Dim EXP As String

            Dim DataType As String
            Dim TResult As TObjects.TObject

            NX_ADDR = 0
            N = 0
            Index = 0

            DataType = String.Empty
            Result = Nothing
            EXP = String.Empty

            If (SYS_ERR_CODE = 0) Then

                Try

                    Select Case INSTR_TYPE

                        Case INSTR_PROG

                            SYS_SCOPE = -1

                            SYS_ARG_INDEX = 0
                            SYS_NX_ADDR = 0

                            SYS_RETURN = False
                            SYS_EXECUTABLE = True

                        Case INSTR_END_PROG

                            SYS_SCOPE = -2

                        Case INSTR_GLOBAL_DEF

                            If (SYS_EXECUTABLE) Then

                                If (IsArray(LEXICONS(1))) Then

                                    If (IsEquStr(LEXICONS(1), KW_NUMBER_ARRAY)) Then
                                        DataType = KW_NUMBER
                                        Result = 0
                                    ElseIf (IsEquStr(LEXICONS(1), KW_BOOLEAN_ARRAY)) Then
                                        DataType = KW_BOOLEAN
                                        Result = KW_FALSE
                                    ElseIf (IsEquStr(LEXICONS(1), KW_OBJECT_ARRAY)) Then
                                        DataType = KW_OBJECT
                                        Result = New TObjects.TObject()
                                    ElseIf (IsEquStr(LEXICONS(1), KW_STRING_ARRAY)) Then
                                        DataType = KW_STRING
                                        Result = ChrW(DL_ST) + ChrW(DL_ST)
                                    End If

                                    TResult = ARRAY_DECLARATION(LEXICONS(2), DataType, Result)

                                    For Index = 3 To LX_INDEX Step 2
                                        V_ADD(LEXICONS(Index), TResult, LEXICONS(1).ToUpper())
                                    Next Index

                                Else

                                    If (IsEquStr(LEXICONS(1), KW_NUMBER)) Then
                                        Result = 0
                                    ElseIf (IsEquStr(LEXICONS(1), KW_OBJECT)) Then
                                        Result = New TObjects.TObject()
                                    ElseIf (IsEquStr(LEXICONS(1), KW_BOOLEAN)) Then
                                        Result = KW_FALSE
                                    ElseIf (IsEquStr(LEXICONS(1), KW_STRING)) Then
                                        Result = ChrW(DL_ST) + ChrW(DL_ST)
                                    End If

                                    For Index = 2 To LX_INDEX Step 2
                                        V_ADD(LEXICONS(Index), Result, LEXICONS(1).ToUpper())
                                    Next Index

                                End If
                            End If

                        Case INSTR_LOCAL_DEF

                            If (SYS_EXECUTABLE) Then

                                If (IsArray(LEXICONS(1))) Then

                                    If (IsEquStr(LEXICONS(1), KW_NUMBER_ARRAY)) Then
                                        DataType = KW_NUMBER
                                        Result = 0
                                    ElseIf (IsEquStr(LEXICONS(1), KW_OBJECT_ARRAY)) Then
                                        DataType = KW_OBJECT
                                        Result = New TObjects.TObject()
                                    ElseIf (IsEquStr(LEXICONS(1), KW_BOOLEAN_ARRAY)) Then
                                        DataType = KW_BOOLEAN
                                        Result = KW_FALSE
                                    ElseIf (IsEquStr(LEXICONS(1), KW_STRING_ARRAY)) Then
                                        DataType = KW_STRING
                                        Result = ChrW(DL_ST) + ChrW(DL_ST)
                                    End If

                                    TResult = ARRAY_DECLARATION(LEXICONS(2), DataType, Result)

                                    For Index = 3 To LX_INDEX Step 2
                                        LV_ADD(LEXICONS(Index), TResult, LEXICONS(1).ToUpper())
                                    Next Index

                                Else

                                    If (IsEquStr(LEXICONS(1), KW_NUMBER)) Then
                                        Result = 0
                                    ElseIf (IsEquStr(LEXICONS(1), KW_OBJECT)) Then
                                        Result = New TObjects.TObject()
                                    ElseIf (IsEquStr(LEXICONS(1), KW_BOOLEAN)) Then
                                        Result = KW_FALSE
                                    ElseIf (IsEquStr(LEXICONS(1), KW_STRING)) Then
                                        Result = ChrW(DL_ST) + ChrW(DL_ST)
                                    End If

                                    For Index = 2 To LX_INDEX Step 2
                                        LV_ADD(LEXICONS(Index), Result, LEXICONS(1).ToUpper())
                                    Next Index

                                End If

                            End If

                        Case INSTR_PROC_DEF

                            SYS_EXECUTABLE = False

                            N = 0

                            For Index = 3 To LX_INDEX

                                If (IsEquStr(LEXICONS(Index), KW_NUMBER) Or
                                    IsEquStr(LEXICONS(Index), KW_BOOLEAN) Or
                                    IsEquStr(LEXICONS(Index), KW_STRING) Or
                                    IsEquStr(LEXICONS(Index), KW_OBJECT) Or
                                    IsEquStr(LEXICONS(Index), KW_OBJECT_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_NUMBER_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_BOOLEAN_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_STRING_ARRAY)) Then

                                    N = N + 1
                                End If

                            Next Index

                            DF_WRITE(SYS_NULL, LEXICONS(2), N, CUR_ADDR)

                        Case INSTR_FUNC_DEF


                            SYS_EXECUTABLE = False

                            N = 0

                            For Index = 4 To LX_INDEX

                                If (IsEquStr(LEXICONS(Index), KW_NUMBER) Or
                                    IsEquStr(LEXICONS(Index), KW_BOOLEAN) Or
                                    IsEquStr(LEXICONS(Index), KW_STRING) Or
                                    IsEquStr(LEXICONS(Index), KW_OBJECT) Or
                                    IsEquStr(LEXICONS(Index), KW_OBJECT_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_NUMBER_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_BOOLEAN_ARRAY) Or
                                    IsEquStr(LEXICONS(Index), KW_STRING_ARRAY)) Then
                                    N = N + 1
                                End If

                            Next Index

                            DF_WRITE(LEXICONS(1), LEXICONS(3), N, CUR_ADDR)

                        Case INSTR_PROC_CALL

                            INC_LC_SCP()

                            If (SYS_ARG_INDEX > 0) Then

                                N = 1

                                For Index = 4 To LX_INDEX Step 3

                                    If (IsArray(LEXICONS(Index))) Then

                                        If (IsEquStr(LEXICONS(Index), KW_NUMBER_ARRAY)) Then
                                            DataType = KW_NUMBER
                                            Result = 0
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_OBJECT_ARRAY)) Then
                                            DataType = KW_OBJECT
                                            Result = New TObjects.TObject()
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_BOOLEAN_ARRAY)) Then
                                            DataType = KW_BOOLEAN
                                            Result = KW_FALSE
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_STRING_ARRAY)) Then
                                            DataType = KW_STRING
                                            Result = ChrW(DL_ST) + ChrW(DL_ST)
                                        End If

                                        TResult = TryCast(SYS_ARGS(N), TObjects.TObject)


                                        LV_ADD(LEXICONS(Index + 1), TResult, LEXICONS(Index).ToUpper())

                                        N = N + 1

                                    ElseIf (IsEquStr(LEXICONS(Index), KW_NUMBER) And (IsNumeric(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_OBJECT) And (IsObjectType(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_BOOLEAN) And (IsBoolean(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_STRING) And (IsString(SYS_ARGS(N)))) Then

                                        LV_ADD(LEXICONS(Index + 1), SYS_ARGS(N))

                                        N = N + 1
                                    End If

                                Next Index

                                SYS_ARG_INDEX = 0


                            End If

                        Case INSTR_FUNC_CALL

                            INC_LC_SCP()

                            If (SYS_ARG_INDEX > 0) Then

                                N = 1

                                For Index = 5 To LX_INDEX Step 3

                                    If (IsArray(LEXICONS(Index))) Then

                                        If (IsEquStr(LEXICONS(Index), KW_NUMBER_ARRAY)) Then
                                            DataType = KW_NUMBER
                                            Result = 0
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_OBJECT_ARRAY)) Then
                                            DataType = KW_OBJECT
                                            Result = New TObjects.TObject()
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_BOOLEAN_ARRAY)) Then
                                            DataType = KW_BOOLEAN
                                            Result = KW_FALSE
                                        ElseIf (IsEquStr(LEXICONS(Index), KW_STRING_ARRAY)) Then
                                            DataType = KW_STRING
                                            Result = ChrW(DL_ST) + ChrW(DL_ST)
                                        End If

                                        TResult = TryCast(SYS_ARGS(N), TObjects.TObject)

                                        LV_ADD(LEXICONS(Index + 1), TResult, LEXICONS(Index).ToUpper())

                                        N = N + 1

                                    ElseIf (IsEquStr(LEXICONS(Index), KW_NUMBER) And (IsNumeric(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_OBJECT) And (IsObjectType(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_BOOLEAN) And (IsBoolean(SYS_ARGS(N))) Or
                                            IsEquStr(LEXICONS(Index), KW_STRING) And (IsString(SYS_ARGS(N)))) Then

                                        LV_ADD(LEXICONS(Index + 1), SYS_ARGS(N))

                                        N = N + 1
                                    End If

                                Next Index

                                SYS_ARG_INDEX = 0
                            End If

                            LV_ADD(SYS_NULL + KW_FUNC + SYS_NULL + CStr(SYS_SCOPE), LEXICONS(1))

                        Case INSTR_END_PROC_CALL

                            If (SYS_SCOPE > 0) Then

                                DEC_LC_SCP()

                                Result = LV_READ(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE))

                                If (Not Result Is SYS_NULL) Then

                                    LV_END = LV_END - 1
                                    ReDim Preserve LVR(LV_END)

                                    NX_ADDR = CLng(Result)

                                    SYS_NX_ADDR = NX_ADDR + 1
                                End If

                            End If

                        Case INSTR_END_FUNC_CALL

                            If (SYS_SCOPE > 0) Then

                                If (OPR(OPR_BASE - 1) = SYS_NULL) Then
                                    OPR(OPR_BASE - 1) = OPR(OPR_PTR)
                                End If

                                DEC_LC_SCP()

                                Result = LV_READ(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE))

                                If (Not Result Is SYS_NULL) Then

                                    LV_END = LV_END - 1
                                    ReDim Preserve LVR(LV_END)

                                    NX_ADDR = CLng(Result)

                                    SYS_NX_ADDR = NX_ADDR

                                    SYS_RETURN = True
                                End If

                            End If

                        Case INSTR_END_PROC
                            SYS_EXECUTABLE = True

                        Case INSTR_END_FUNC
                            SYS_EXECUTABLE = True

                        Case INSTR_FUNC_RTR

                            If (SYS_RETURN) Then
                                EVALUATE(Result, NX_ADDR, POX_PTR)
                                SYS_RETURN = False
                            Else
                                POST_FIX(2)
                                EVALUATE(Result, NX_ADDR)
                            End If

                            If ((Result Is SYS_NULL) And (NX_ADDR <> 0)) Then
                                SYS_NX_ADDR = NX_ADDR
                                LV_ADD(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))

                            ElseIf (Not Result Is SYS_NULL) Then

                                OPR_PTR = OPR_PTR + 1
                                ReDim Preserve OPR(OPR_PTR)

                                OPR(OPR_PTR) = Result

                            End If

                        Case INSTR_PROC_MAIN

                            SYS_SCOPE = 0
                            LV_PTR = 1
                            LV_END = 1
                            LV_START = 1

                            POX_START = 1
                            OPR_BASE = 1

                        Case INSTR_IF

                            If (SYS_EXECUTABLE) Then

                                If (SYS_RETURN) Then
                                    EVALUATE(Result, NX_ADDR, POX_PTR)
                                    SYS_RETURN = False
                                Else
                                    POST_FIX(2)
                                    EVALUATE(Result, NX_ADDR)
                                End If

                                If ((Result = SYS_NULL) And (NX_ADDR <> 0)) Then
                                    SYS_NX_ADDR = NX_ADDR
                                    LV_ADD(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))

                                ElseIf (IsBoolean(Result)) Then

                                    INC_BL_SCP()
                                    LV_ADD(SYS_NULL + KW_IF + SYS_NULL + CStr(SYS_SCOPE), Result)

                                End If
                            Else
                                SYS_SCOPE = SYS_SCOPE + 1
                            End If

                        Case INSTR_THEN

                            Result = LV_READ(SYS_NULL + KW_IF + SYS_NULL + CStr(SYS_SCOPE))

                            If (Result <> SYS_NULL) Then
                                SYS_EXECUTABLE = CBool(Result)
                            Else
                                SYS_EXECUTABLE = False
                            End If

                        Case INSTR_ELSE

                            Result = LV_READ(SYS_NULL + KW_IF + SYS_NULL + CStr(SYS_SCOPE))

                            If (Not Result Is SYS_NULL) Then
                                SYS_EXECUTABLE = Not (CBool(Result))
                            Else
                                SYS_EXECUTABLE = False
                            End If

                        Case INSTR_END_IF

                            Result = LV_READ(SYS_NULL + KW_IF + SYS_NULL + CStr(SYS_SCOPE))

                            If (Not Result Is SYS_NULL) Then
                                SYS_EXECUTABLE = True
                                DEC_BL_SCP()
                            Else
                                SYS_SCOPE = SYS_SCOPE - 1
                            End If

                        Case INSTR_WHILE

                            If (SYS_EXECUTABLE) Then

                                If (SYS_RETURN) Then
                                    EVALUATE(Result, NX_ADDR, POX_PTR)
                                    SYS_RETURN = False
                                Else
                                    POST_FIX(2)
                                    EVALUATE(Result, NX_ADDR)
                                End If

                                If ((Result Is SYS_NULL) And (NX_ADDR <> 0)) Then
                                    SYS_NX_ADDR = NX_ADDR
                                    LV_ADD(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))

                                ElseIf (IsBoolean(Result)) Then

                                    INC_BL_SCP()

                                    If (CBool(Result)) Then

                                        LV_ADD(SYS_NULL + KW_WHILE + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))
                                        SYS_EXECUTABLE = True
                                    Else

                                        LV_ADD(SYS_NULL + KW_WHILE + SYS_NULL + CStr(SYS_SCOPE), 0)
                                        SYS_EXECUTABLE = False
                                    End If
                                End If
                            Else
                                SYS_SCOPE = SYS_SCOPE + 1
                            End If

                        Case INSTR_END_WHILE

                            Result = LV_READ(SYS_NULL + KW_WHILE + SYS_NULL + CStr(SYS_SCOPE))

                            If (Not Result Is SYS_NULL) Then

                                SYS_EXECUTABLE = True
                                SYS_NX_ADDR = CLng(Result)
                                DEC_BL_SCP()
                            Else
                                SYS_SCOPE = SYS_SCOPE - 1
                            End If

                        Case INSTR_REPEAT

                            If (SYS_EXECUTABLE) Then

                                INC_BL_SCP()
                                LV_ADD(SYS_NULL + KW_REPEAT + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))
                            Else
                                SYS_SCOPE = SYS_SCOPE + 1
                            End If

                        Case INSTR_UNTIL

                            Result = LV_READ(SYS_NULL + KW_REPEAT + SYS_NULL + CStr(SYS_SCOPE))

                            If (Not Result Is SYS_NULL) Then

                                If (SYS_RETURN) Then
                                    EVALUATE(EXP, NX_ADDR, POX_PTR)
                                    SYS_RETURN = False
                                Else
                                    POST_FIX(2)
                                    EVALUATE(EXP, NX_ADDR)
                                End If

                                If ((EXP Is SYS_NULL) And (NX_ADDR <> 0)) Then
                                    SYS_NX_ADDR = NX_ADDR
                                    LV_ADD(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))

                                ElseIf (Not (CBool(EXP))) Then
                                    SYS_NX_ADDR = CLng(Result)
                                Else
                                    SYS_NX_ADDR = 0
                                End If

                                SYS_EXECUTABLE = True
                                DEC_BL_SCP()
                            Else
                                SYS_SCOPE = SYS_SCOPE - 1
                            End If

                        Case INSTR_EXP

                            If (SYS_EXECUTABLE) Then

                                If (SYS_RETURN) Then
                                    EVALUATE(Result, NX_ADDR, POX_PTR)
                                    SYS_RETURN = False
                                Else
                                    POST_FIX(1)
                                    EVALUATE(Result, NX_ADDR)
                                End If

                                If ((Result Is SYS_NULL) And (NX_ADDR <> 0)) Then
                                    SYS_NX_ADDR = NX_ADDR

                                    LV_ADD(SYS_NULL + KW_RETURN + SYS_NULL + CStr(SYS_SCOPE), CStr(CUR_ADDR))

                                End If

                            End If

                        Case Else

                            SYS_NX_ADDR = 0
                    End Select

                Catch ex As Exception

                    SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
                End Try

            End If

        End Sub

        Private Sub INC_LC_SCP()

            Try
                SYS_SCOPE = SYS_SCOPE + 1

                ReDim Preserve LV_INDICES(SYS_SCOPE)
                ReDim Preserve OPR_INDICES(SYS_SCOPE)

                LV_INDICES(SYS_SCOPE).LV_STR = LV_START
                LV_INDICES(SYS_SCOPE).LV_PTR = LV_PTR

                OPR_INDICES(SYS_SCOPE).OPR_BASE = OPR_BASE
                OPR_INDICES(SYS_SCOPE).POX_PTR = POX_PTR
                OPR_INDICES(SYS_SCOPE).POX_STR = POX_START

                If (LV_END > 1) Then
                    LV_START = LV_END + 1
                    LV_END = LV_START
                    LV_PTR = LV_START
                End If

                If (OPR_PTR >= OPR_BASE) Then
                    OPR_BASE = OPR_PTR + 1
                End If

                If (POX_END >= POX_START) Then
                    POX_START = POX_END + 1
                    POX_PTR = POX_START
                End If

            Catch ex As Exception

                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
            End Try

        End Sub

        Private Sub DEC_LC_SCP()

            Try
                If (LV_START > 1) Then
                    LV_END = LV_START - 1
                Else
                    LV_END = LV_START
                End If

                If (OPR_BASE > 1) Then
                    OPR_PTR = OPR_BASE - 1
                Else
                    OPR_PTR = OPR_BASE
                End If

                If (POX_START > 1) Then
                    POX_END = POX_START - 1
                Else
                    POX_END = POX_START
                End If

                ReDim Preserve OPR(OPR_PTR)
                ReDim Preserve POX(POX_END)
                ReDim Preserve LVR(LV_END)

                If (Not LV_INDICES Is Nothing) Then
                    LV_START = LV_INDICES(SYS_SCOPE).LV_STR
                    LV_PTR = LV_INDICES(SYS_SCOPE).LV_PTR
                Else
                    SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
                End If

                If (Not OPR_INDICES Is Nothing) Then
                    OPR_BASE = OPR_INDICES(SYS_SCOPE).OPR_BASE
                    POX_PTR = OPR_INDICES(SYS_SCOPE).POX_PTR
                    POX_START = OPR_INDICES(SYS_SCOPE).POX_STR
                Else
                    SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
                End If

                SYS_SCOPE = SYS_SCOPE - 1

                If (SYS_SCOPE = 0) Then Exit Sub

                ReDim Preserve LV_INDICES(SYS_SCOPE)
                ReDim Preserve OPR_INDICES(SYS_SCOPE)

            Catch ex As Exception
                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
            End Try

        End Sub

        Private Sub INC_BL_SCP()
            Try

                SYS_SCOPE = SYS_SCOPE + 1

                ReDim Preserve LV_INDICES(SYS_SCOPE)

                LV_INDICES(SYS_SCOPE).LV_STR = LV_START
                LV_INDICES(SYS_SCOPE).LV_PTR = LV_PTR

                If (LV_END > 1) Then
                    LV_PTR = LV_END + 1
                    LV_END = LV_PTR
                End If

            Catch ex As Exception
                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
            End Try

        End Sub

        Private Sub DEC_BL_SCP()

            Try
                If (SYS_SCOPE > 0) Then

                    If (LV_PTR > 1) Then
                        LV_END = LV_PTR - 1
                    Else
                        LV_END = LV_PTR
                    End If

                    ReDim Preserve LVR(LV_END)

                    If (Not LV_INDICES Is Nothing) Then
                        LV_PTR = LV_INDICES(SYS_SCOPE).LV_PTR
                    Else
                        SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
                    End If

                    SYS_SCOPE = SYS_SCOPE - 1

                    If (SYS_SCOPE = 0) Then Exit Sub

                    ReDim Preserve LV_INDICES(SYS_SCOPE)

                End If

            Catch ex As Exception
                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
            End Try

        End Sub

    End Module

End Namespace