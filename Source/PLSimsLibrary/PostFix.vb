Namespace PLSims

    Public Module PostFix


        'This module contain the functionality to control
        'polish conversion and related operations

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        Private Function PRECEDENCE(Optr As String) As Integer

            If (IsResolver(Optr) Or IsArraySubscript(Optr)) Then
                PRECEDENCE = 10
            ElseIf IsUnary(Optr) Then
                PRECEDENCE = 9
            ElseIf (Optr = ChrW(DL_PW)) Then
                PRECEDENCE = 8
            ElseIf ((Optr = ChrW(DL_MU)) Or (Optr = ChrW(DL_DI))) Then
                PRECEDENCE = 7
            ElseIf (Optr = ChrW(DL_MD)) Then
                PRECEDENCE = 6
            ElseIf ((Optr = ChrW(DL_PL)) Or (Optr = ChrW(DL_MI))) Then
                PRECEDENCE = 5
            ElseIf IsRelational(Optr) Then
                PRECEDENCE = 4
            ElseIf IsLogical(Optr) Then
                PRECEDENCE = 3
            ElseIf IsAssignment(Optr) Then
                PRECEDENCE = 2
            ElseIf IsSeperator(Optr) Then
                PRECEDENCE = 1
            Else
                PRECEDENCE = 0
            End If

        End Function

        Public Sub POST_FIX(BEG_LX_INDEX As Integer)

            Dim PL_STACK() As String

            Dim PTR As Integer
            Dim Index As Integer

            Dim Opcode As String

            'For Debugging
            Dim BracketCount As Integer

            BracketCount = 1
            POX_END = POX_START - 1

            PTR = 1
            ReDim Preserve PL_STACK(PTR)

            PL_STACK(PTR) = ChrW(DL_OB)

            LX_INDEX = LX_INDEX + 1
            ReDim Preserve LEXICONS(LX_INDEX)

            LEXICONS(LX_INDEX) = ChrW(DL_CB)

            For Index = BEG_LX_INDEX To LX_INDEX
                Opcode = LEXICONS(Index)

                If IsOBracket(Opcode) Or IsFunction(Opcode) Or _
                   IsProcedure(Opcode) Or IsUsrDef(Opcode) Or _
                   IsUnary(Opcode) Then

                    If IsOBracket(Opcode) Then
                        BracketCount = BracketCount + 1
                    End If

                    PTR = PTR + 1

                    ReDim Preserve PL_STACK(PTR)

                    PL_STACK(PTR) = Opcode

                ElseIf (IsOperators(Opcode) Or IsArraySubscript(Opcode)) Then

                    Dim PreOpcode As Integer
                    Dim PreStack As Integer

                    PreOpcode = PRECEDENCE(Opcode)
                    PreStack = PRECEDENCE(PL_STACK(PTR))

                    While (PreStack >= PreOpcode)

                        POX_END = POX_END + 1

                        ReDim Preserve POX(POX_END)
                        POX(POX_END) = PL_STACK(PTR)

                        PTR = PTR - 1
                        ReDim Preserve PL_STACK(PTR)

                        PreStack = PRECEDENCE(PL_STACK(PTR))

                    End While

                    PTR = PTR + 1

                    ReDim Preserve PL_STACK(PTR)
                    PL_STACK(PTR) = Opcode

                ElseIf IsCBracket(Opcode) Then

                    BracketCount = BracketCount - 1

                    While Not (IsOBracket(PL_STACK(PTR)))

                        POX_END = POX_END + 1
                        ReDim Preserve POX(POX_END)

                        POX(POX_END) = PL_STACK(PTR)

                        PTR = PTR - 1
                        ReDim Preserve PL_STACK(PTR)

                    End While

                    If (PTR > 1) Then

                        PTR = PTR - 1
                        ReDim Preserve PL_STACK(PTR)

                    End If

                    If IsFunction(PL_STACK(PTR)) Or _
                       IsProcedure(PL_STACK(PTR)) Or _
                       IsUsrDef(PL_STACK(PTR)) Then

                        POX_END = POX_END + 1
                        ReDim Preserve POX(POX_END)

                        POX(POX_END) = PL_STACK(PTR)

                        PTR = PTR - 1
                        ReDim Preserve PL_STACK(PTR)

                    End If

                Else
                    POX_END = POX_END + 1
                    ReDim Preserve POX(POX_END)

                    POX(POX_END) = Opcode

                End If
            Next

            If (BracketCount > 0) Then
                SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_BRACKET
            End If

        End Sub

       

    End Module

End Namespace