
Namespace PLSims

    Public Module Interpretation


        'This module contains the functionality to control
        'tokenizing, polish conversion, lexical analysis,
        'syntatic analysis, and semantic analysis

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'Constants Declaration

        'Instruction Type Constants
        Public Const INSTR_PROG = 0
        Public Const INSTR_END_PROG = 1
        Public Const INSTR_GLOBAL_DEF = 2
        Public Const INSTR_LOCAL_DEF = 3
        Public Const INSTR_PROC_DEF = 4
        Public Const INSTR_FUNC_DEF = 5
        Public Const INSTR_PROC_CALL = 6
        Public Const INSTR_FUNC_CALL = 7
        Public Const INSTR_END_PROC_CALL = 8
        Public Const INSTR_END_FUNC_CALL = 9
        Public Const INSTR_END_PROC = 10
        Public Const INSTR_END_FUNC = 11
        Public Const INSTR_FUNC_RTR = 12
        Public Const INSTR_PROC_MAIN = 13
        Public Const INSTR_IF = 14
        Public Const INSTR_THEN = 15
        Public Const INSTR_ELSE = 16
        Public Const INSTR_END_IF = 17
        Public Const INSTR_WHILE = 18
        Public Const INSTR_END_WHILE = 19
        Public Const INSTR_REPEAT = 20
        Public Const INSTR_UNTIL = 21
        Public Const INSTR_EXP = 22
        Public Const INSTR_NOP = 23


        'White Space Constants
        Public Const WS_TB = 9    ' Tab
        Public Const WS_LF = 10   ' Line Feed
        Public Const WS_FF = 11   ' Form Feed
        Public Const WS_VT = 12   ' Vetrical Tab
        Public Const WS_CR = 13   ' Carriage Return
        Public Const WS_SP = 32   ' Space

        'Delimiter Constants
        Public Const DL_OB = 40     ' Open Bracket
        Public Const DL_CB = 41     ' Close Bracket
        Public Const DL_OA = 91     ' Open Angle Bracket
        Public Const DL_CA = 93     ' Close Angle Bracket
        Public Const DL_OC = 123    ' Open Curly Bracket
        Public Const DL_CC = 125    ' Close Curly Bracket
        Public Const DL_PW = 94     ' Power
        Public Const DL_MU = 42     ' Multiply
        Public Const DL_DI = 47     ' Divide
        Public Const DL_PL = 43     ' Plus
        Public Const DL_MI = 45     ' Minus
        Public Const DL_MD = 37     ' Modulus
        Public Const DL_UM = 126    ' Unary Minus
        Public Const DL_GT = 62     ' Greater Than
        Public Const DL_LT = 60     ' Less Than
        Public Const DL_EQ = 61     ' Equal To
        Public Const DL_AS = 58     ' Assignment
        Public Const DL_ST = 34     ' String
        Public Const DL_SP = 44     ' Seperator
        Public Const DL_CM = 64     ' Comment
        Public Const DL_PR = 46     ' Period
        Public Const DL_SC = 59     ' SemiColon

        'Keyword Constants

        'Data Type Constants
        Public Const KW_NUMBER = "NUMBER"
        Public Const KW_BOOLEAN = "BOOLEAN"
        Public Const KW_STRING = "STRING"
        Public Const KW_ARRAY = "ARRAY"
        Public Const KW_OBJECT = "OBJECT"

        Public Const KW_NUMBER_ARRAY = "NUMBERARRAY"
        Public Const KW_BOOLEAN_ARRAY = "BOOLEANARRAY"
        Public Const KW_STRING_ARRAY = "STRINGARRAY"
        Public Const KW_OBJECT_ARRAY = "OBJECTARRAY"

        'Logical Constants
        Public Const KW_AND = "AND"
        Public Const KW_OR = "OR"
        Public Const KW_NOT = "NOT"
        Public Const KW_XOR = "XOR"

        'Boolean Constants
        Public Const KW_TRUE = "TRUE"
        Public Const KW_FALSE = "FALSE"

        'Procedure Constants
        Public Const KW_PROC = "PROCEDURE"
        Public Const KW_FUNC = "FUNCTION"
        Public Const KW_PROC_MAIN = "PROCEDUREMAIN"
        Public Const KW_RETURN = "RETURN"
        Public Const KW_END_PROC = "ENDPROCEDURE"
        Public Const KW_END_FUNC = "ENDFUNCTION"

        'Sequence Control Constants
        Public Const KW_IF = "IF"
        Public Const KW_THEN = "THEN"
        Public Const KW_ELSE = "ELSE"
        Public Const KW_END_IF = "ENDIF"

        'Iteration Control Constants
        Public Const KW_WHILE = "WHILE"
        Public Const KW_END_WHILE = "ENDWHILE"
        Public Const KW_REPEAT = "REPEAT"
        Public Const KW_UNTIL = "UNTIL"

        'Termination Constants
        Public Const KW_END = "END"

        'Program Constants
        Public Const KW_PROG = "PROGRAM"
        Public Const KW_END_PROG = "ENDPROGRAM"

        'Primary Procedure Constants
        Public Const KW_MAIN = "MAIN"

        'Escape Character Constant
        Public Const KW_ENTER = "ENTER"
        Public Const KW_SPACE = "SPACE"
        Public Const KW_TAB = "TAB"

        'Escape Web Character Constant
        Public Const KW_BR = "BR"
        Public Const KW_NPSP = "NBSP"


        'NULL Constants
        Public Const KW_VOID = "VOID"
        Public Const KW_NULL = "NULL"
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'KeyWords
        Public KEYWORDS As String() = {KW_NUMBER, KW_BOOLEAN, KW_STRING, KW_ARRAY, KW_OBJECT, KW_NUMBER_ARRAY, KW_BOOLEAN_ARRAY, KW_STRING_ARRAY, KW_OBJECT_ARRAY, KW_AND, KW_OR, KW_NOT, KW_XOR, KW_TRUE, KW_FALSE, KW_PROC, KW_FUNC, KW_PROC_MAIN, KW_RETURN, KW_END_PROC, KW_END_FUNC, KW_IF, KW_THEN, KW_ELSE, KW_END_IF, KW_WHILE, KW_END_WHILE, KW_REPEAT, KW_UNTIL, KW_END, KW_PROG, KW_END_PROG, KW_MAIN, KW_ENTER, KW_SPACE, KW_TAB, KW_BR, KW_NPSP, KW_VOID, KW_NULL}

        'Variables Declaration

        Private TOKENS() As String
        Public LEXICONS() As String

        Public TK_INDEX As Integer  'Token Index
        Public LX_INDEX As Integer  'Lexicon Index

        Public Function Subscript(Exp As String) As String

            Dim Index As Integer
            Dim Token As String

            Dim ArrayList As String

            ArrayList = ChrW(DL_OA)

            Dim Tokens() As String = Exp.Split(ChrW(DL_OA), ChrW(DL_CA))

            Token = String.Empty

            For Index = 0 To Tokens.Length - 1

                Token = Tokens(Index)

                If (Not String.IsNullOrEmpty(Token)) Then

                    ArrayList += Token + ChrW(DL_SP)
                End If

            Next

            ArrayList += ChrW(DL_CA)

            Return ArrayList

        End Function


        Public Function Indices(Exp As String) As List(Of Integer)

            Dim ArrayIndices As List(Of Integer)

            Dim Index As Integer
            Dim Token As String
            Dim Value As Integer

            Dim ArrayTokens() As String

            ArrayIndices = New List(Of Integer)

            ArrayTokens = Exp.Substring(1, Exp.Length - 2).Split(ChrW(DL_SP))

            Token = String.Empty
            Value = 0

            For Index = 0 To ArrayTokens.Length - 1

                Token = ArrayTokens(Index)

                If (Not String.IsNullOrEmpty(Token)) Then

                    If (IsNumeric(Token)) Then
                        ArrayIndices.Add(Convert.ToInt32(Token))
                    Else
                        Value = MEM_READ(Token)
                        ArrayIndices.Add(Convert.ToInt32(Value))
                    End If
                End If


            Next

            Return ArrayIndices

        End Function

        Public Function ARRAY_DECLARATION(Exp As String, Type As String, InitialValue As Object) As TObjects.TObject

            Return New TObjects.TObject()

        End Function

        Private Sub TOKENIZE(ByRef EXP As String)

            Dim Index As Integer
            Dim POS As Integer
            Dim ch As String
            Dim ST As String

            Dim ArrayList As String

            ArrayList = String.Empty

            TK_INDEX = 0

            For Index = 1 To Len(EXP)

                ch = Mid(EXP, Index, 1)

                If IsWhiteSpace(ch) Then

                    If (ST <> vbNullString) Then

                        TK_INDEX = TK_INDEX + 1
                        ReDim Preserve TOKENS(TK_INDEX)

                        TOKENS(TK_INDEX) = ST
                    End If

                    ST = vbNullString

                ElseIf IsComment(ch) Then

                    If (ST <> vbNullString) Then

                        TK_INDEX = TK_INDEX + 1
                        ReDim Preserve TOKENS(TK_INDEX)

                        TOKENS(TK_INDEX) = ST

                    End If

                    'POS = InStr(Index + 1, EXP, ChrW(DL_CM))
                    POS = Len(EXP)

                    ST = vbNullString

                    Index = POS + 1

                ElseIf IsString(ch) Then

                    If (ST <> vbNullString) Then

                        TK_INDEX = TK_INDEX + 1
                        ReDim Preserve TOKENS(TK_INDEX)

                        TOKENS(TK_INDEX) = ST

                    End If

                    POS = InStr(Index + 1, EXP, ChrW(DL_ST))

                    'Error: String End Delimiter Not Found
                    If (POS < Index + 1) Then

                        SYS_ERR_CODE = ErrorType.SYS_ERROR_STRING_NOT_FOUND
                        Exit Sub

                    End If

                    ST = ch + Mid$(EXP, Index + 1, POS - Index)

                    TK_INDEX = TK_INDEX + 1
                    ReDim Preserve TOKENS(TK_INDEX)

                    TOKENS(TK_INDEX) = ST

                    ST = vbNullString

                    Index = POS

                ElseIf IsOAngleBracket(ch) Then

                    If (ST <> vbNullString) Then

                        TK_INDEX = TK_INDEX + 1
                        ReDim Preserve TOKENS(TK_INDEX)

                        TOKENS(TK_INDEX) = ST

                    End If

                    POS = InStr(Index + 1, EXP, ChrW(DL_CA))

                    'Error: Array End Delimiter Not Found
                    If (POS < Index + 1) Then

                        SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_ANGLE_BRACKET
                        Exit Sub

                    End If

                    ST = ch + Mid$(EXP, Index + 1, POS - Index)

                    TK_INDEX = TK_INDEX + 1
                    ReDim Preserve TOKENS(TK_INDEX)

                    TOKENS(TK_INDEX) = ST

                    ST = vbNullString

                    Index = POS

                ElseIf IsDelimiter(ch) Then

                    If (ST <> vbNullString) Then

                        TK_INDEX = TK_INDEX + 1
                        ReDim Preserve TOKENS(TK_INDEX)

                        TOKENS(TK_INDEX) = ST

                    End If

                    TK_INDEX = TK_INDEX + 1
                    ReDim Preserve TOKENS(TK_INDEX)

                    TOKENS(TK_INDEX) = ch

                    ST = vbNullString

                Else
                    ST = ST + ch
                End If

            Next Index

            If (ST <> vbNullString) Then

                TK_INDEX = TK_INDEX + 1
                ReDim Preserve TOKENS(TK_INDEX)

                TOKENS(TK_INDEX) = ST
            End If

        End Sub

        Private Sub LEXICAL_ANALYSIS()

            Dim Index As Long
            Dim Lexicon As String
            Dim Prev_Lexicon As String
            Dim Next_Lexicon As String

            Dim ArrayList As String

            ArrayList = String.Empty

            LX_INDEX = 0

            Prev_Lexicon = String.Empty
            Next_Lexicon = String.Empty

            For Index = 1 To TK_INDEX
                Lexicon = TOKENS(Index)

                If (Index < TK_INDEX) Then
                    Next_Lexicon = TOKENS(Index + 1)
                End If

                If (Index > 1) Then
                    Prev_Lexicon = TOKENS(Index - 1)
                End If

                If (IsOperators(Lexicon + Next_Lexicon)) Then
                    Lexicon = Lexicon + Next_Lexicon

                    Index = Index + 1

                ElseIf (Next_Lexicon = ChrW(DL_PR)) Then

                    If (IsNumeric(Lexicon)) Then
                        Continue For
                    End If

                ElseIf (Lexicon = ChrW(DL_PR)) Then

                    If (IsNumeric(Lexicon + Next_Lexicon)) Then
                        Lexicon = Prev_Lexicon + Lexicon + Next_Lexicon
                        Prev_Lexicon = Nothing
                        Next_Lexicon = Nothing

                        Index = Index + 1
                    Else
                        Lexicon = ChrW(DL_SC)
                    End If
                ElseIf (IsArraySubscript(Lexicon)) Then

                    ArrayList += Lexicon

                    If (IsArraySubscript(Next_Lexicon) And Index < TK_INDEX) Then
                        Continue For
                    Else

                        Lexicon = Subscript(ArrayList)
                        ArrayList = ""

                    End If

                ElseIf (IsArray(Lexicon + Next_Lexicon)) Then
                    Lexicon = Lexicon + Next_Lexicon

                    Index = Index + 1

                ElseIf (IsString(Lexicon)) Then

                    'Do Nothing

                ElseIf (Lexicon = ChrW(DL_MI)) Then

                    If (IsArithmetic(Prev_Lexicon) Or _
                    IsRelational(Prev_Lexicon) Or _
                    IsOBracket(Prev_Lexicon) Or _
                    IsAssignment(Prev_Lexicon) Or _
                    Prev_Lexicon = ChrW(DL_EQ)) Then

                        Lexicon = ChrW(DL_UM)
                    End If

                ElseIf (IsEquStr(Lexicon, KW_END)) Then

                    If (IsEquStr(Next_Lexicon, KW_PROC) Or _
                    IsEquStr(Next_Lexicon, KW_FUNC) Or _
                    IsEquStr(Next_Lexicon, KW_IF) Or _
                    IsEquStr(Next_Lexicon, KW_PROG) Or _
                    IsEquStr(Next_Lexicon, KW_WHILE)) Then

                        Lexicon = Lexicon + Next_Lexicon

                        Index = Index + 1
                    Else

                        SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_PROGRAM_STRUCT

                    End If



                ElseIf (IsEquStr(Lexicon, KW_PROC)) Then

                    If (IsEquStr(Next_Lexicon, KW_MAIN)) Then
                        Lexicon = Lexicon + Next_Lexicon

                        Index = Index + 1
                    End If

                End If

                LX_INDEX = LX_INDEX + 1
                ReDim Preserve LEXICONS(LX_INDEX)

                LEXICONS(LX_INDEX) = Lexicon

            Next Index

            TK_INDEX = 0

            ReDim TOKENS(1)

        End Sub

        Private Function SEMANTIC_ANALYSIS() As Integer

            Dim Index As Long
            Dim Lexicon As String
            Dim Next_Lexicon As String
            Dim Prev_Lexicon As String

            Next_Lexicon = String.Empty
            Prev_Lexicon = String.Empty

            For Index = 1 To LX_INDEX
                Lexicon = LEXICONS(Index)

                If (Index < LX_INDEX) Then
                    Next_Lexicon = LEXICONS(Index + 1)
                End If

                If (Index > 1) Then
                    Prev_Lexicon = LEXICONS(Index - 1)
                End If

                If (SYS_SCOPE = -2) Then

                    If (IsEquStr(Lexicon, KW_PROG)) Then
                        SEMANTIC_ANALYSIS = INSTR_PROG
                    Else
                        SEMANTIC_ANALYSIS = INSTR_NOP

                        SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_PROGRAM_STRUCT

                    End If

                    Exit Function

                ElseIf (SYS_SCOPE = -1) Then

                    If (IsEquStr(Lexicon, KW_NUMBER) Or _
                        IsEquStr(Lexicon, KW_BOOLEAN) Or _
                        IsEquStr(Lexicon, KW_OBJECT) Or _
                        IsEquStr(Lexicon, KW_STRING) Or _
                        IsArray(Lexicon)) Then

                        If (IsEquStr(Next_Lexicon, KW_FUNC)) Then
                            SEMANTIC_ANALYSIS = INSTR_FUNC_DEF
                        ElseIf (Not IsNumeric(Next_Lexicon)) Then
                            SEMANTIC_ANALYSIS = INSTR_GLOBAL_DEF
                        Else
                            SEMANTIC_ANALYSIS = INSTR_NOP

                            SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_PROGRAM_STRUCT
                        End If

                        Exit Function

                    ElseIf (IsEquStr(Lexicon, KW_PROC)) Then
                        SEMANTIC_ANALYSIS = INSTR_PROC_DEF

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_PROC_MAIN)) Then
                        SEMANTIC_ANALYSIS = INSTR_PROC_MAIN

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_PROC)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_PROC

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_FUNC)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_FUNC

                        Exit Function

                    ElseIf (IsEquStr(Lexicon, KW_END_PROG)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_PROG

                        Exit Function
                    Else
                        SEMANTIC_ANALYSIS = INSTR_NOP

                        Exit Function
                    End If

                ElseIf (SYS_SCOPE > -1) Then

                    If (IsEquStr(Lexicon, KW_NUMBER) Or _
                        IsEquStr(Lexicon, KW_BOOLEAN) Or _
                        IsEquStr(Lexicon, KW_OBJECT) Or _
                        IsEquStr(Lexicon, KW_STRING) Or _
                        IsArray(Lexicon)) Then

                        If (IsEquStr(Next_Lexicon, KW_FUNC)) Then
                            SEMANTIC_ANALYSIS = INSTR_FUNC_CALL
                        Else
                            SEMANTIC_ANALYSIS = INSTR_LOCAL_DEF
                        End If

                        Exit Function

                    ElseIf (IsEquStr(Lexicon, KW_PROC)) Then
                        SEMANTIC_ANALYSIS = INSTR_PROC_CALL

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_PROC)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_PROC_CALL

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_FUNC)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_FUNC_CALL

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_IF)) Then
                        SEMANTIC_ANALYSIS = INSTR_IF

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_THEN)) Then
                        SEMANTIC_ANALYSIS = INSTR_THEN

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_ELSE)) Then
                        SEMANTIC_ANALYSIS = INSTR_ELSE

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_IF)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_IF

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_WHILE)) Then
                        SEMANTIC_ANALYSIS = INSTR_WHILE

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_END_WHILE)) Then
                        SEMANTIC_ANALYSIS = INSTR_END_WHILE

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_REPEAT)) Then
                        SEMANTIC_ANALYSIS = INSTR_REPEAT

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_UNTIL)) Then
                        SEMANTIC_ANALYSIS = INSTR_UNTIL

                        Exit Function
                    ElseIf (IsEquStr(Lexicon, KW_RETURN)) Then
                        SEMANTIC_ANALYSIS = INSTR_FUNC_RTR

                        Exit Function
                    Else
                        SEMANTIC_ANALYSIS = INSTR_EXP

                        Exit Function
                    End If
                End If

            Next Index

            SEMANTIC_ANALYSIS = INSTR_NOP

        End Function

        Public Function IsArithmetic(ch As String) As Boolean

            If ((ch = ChrW(DL_PW)) Or (ch = ChrW(DL_MU)) Or (ch = ChrW(DL_DI)) Or (ch = ChrW(DL_PL)) Or _
               (ch = ChrW(DL_MI)) Or (ch = ChrW(DL_MD))) Then
                IsArithmetic = True
            Else
                IsArithmetic = False
            End If

        End Function

        Public Function IsRelational(ch As String) As Boolean

            If ((ch = ChrW(DL_GT)) Or (ch = ChrW(DL_LT)) Or (ch = ChrW(DL_EQ)) Or _
            (ch = (ChrW(DL_LT) + ChrW(DL_GT))) Or (ch = (ChrW(DL_GT) + ChrW(DL_EQ))) Or _
            (ch = (ChrW(DL_LT) + ChrW(DL_EQ)))) Then

                IsRelational = True
            Else
                IsRelational = False
            End If

        End Function

        Public Function IsLogical(ch As String) As Boolean

            If (IsEquStr(ch, KW_AND) Or IsEquStr(ch, KW_OR) Or _
            IsEquStr(ch, KW_XOR)) Then
                IsLogical = True
            Else
                IsLogical = False
            End If

        End Function

        Public Function IsPeriod(ch As String) As Boolean
            If (ch = ChrW(DL_PR)) Then
                IsPeriod = True
            Else
                IsPeriod = False
            End If
        End Function

        Public Function IsResolver(ch As String) As Boolean
            If (ch = ChrW(DL_SC)) Then
                IsResolver = True
            Else
                IsResolver = False
            End If
        End Function

        Public Function IsUnary(ch As String) As Boolean
            If (ch = ChrW(DL_UM)) Then
                IsUnary = True
            Else
                IsUnary = False
            End If
        End Function

        Public Function IsAssignment(ch As String) As Boolean
            If ((ch = ChrW(DL_AS)) Or (ch = (ChrW(DL_AS) + ChrW(DL_EQ)))) Then
                IsAssignment = True
            Else
                IsAssignment = False
            End If
        End Function

        Public Function IsSeperator(ch As String) As Boolean
            If (ch = ChrW(DL_SP)) Then
                IsSeperator = True
            Else
                IsSeperator = False
            End If
        End Function

        Public Function IsOperators(Opt As String) As Boolean
            If (IsRelational(Opt) Or IsLogical(Opt) Or _
                IsArithmetic(Opt) Or IsSeperator(Opt) Or _
                IsAssignment(Opt) Or IsResolver(Opt)) Then
                IsOperators = True
            Else
                IsOperators = False
            End If
        End Function

        Public Function IsOBracket(ch As String) As Boolean
            If (ch = ChrW(DL_OB)) Then
                IsOBracket = True
            Else
                IsOBracket = False
            End If
        End Function

        Public Function IsCBracket(ch As String) As Boolean

            If (ch = ChrW(DL_CB)) Then
                IsCBracket = True
            Else
                IsCBracket = False
            End If

        End Function

        Public Function IsOAngleBracket(ch As String) As Boolean

            If (ch = ChrW(DL_OA)) Then
                IsOAngleBracket = True
            Else
                IsOAngleBracket = False
            End If

        End Function

        Public Function IsCAngleBracket(ch As String) As Boolean

            If (ch = ChrW(DL_CA)) Then
                IsCAngleBracket = True
            Else
                IsCAngleBracket = False
            End If

        End Function

        Public Function IsOCurlyBracket(ch As String) As Boolean

            If (ch = ChrW(DL_OC)) Then
                IsOCurlyBracket = True
            Else
                IsOCurlyBracket = False
            End If

        End Function

        Public Function IsCCurlyBracket(ch As String) As Boolean

            If (ch = ChrW(DL_CC)) Then
                IsCCurlyBracket = True
            Else
                IsCCurlyBracket = False
            End If

        End Function

        Public Function IsDelimiter(ch As String) As Boolean

            If (IsArithmetic(ch) Or IsLogical(ch) Or IsRelational(ch) Or _
                IsOBracket(ch) Or IsCBracket(ch) Or IsPeriod(ch) Or _
                IsOCurlyBracket(ch) Or IsCCurlyBracket(ch) Or _
                IsSeperator(ch) Or _
                IsAssignment(ch)) Then

                IsDelimiter = True
            Else
                IsDelimiter = False
            End If

        End Function

        Public Function IsString(ch As Object) As Boolean

            If (Not IsObjectType(ch)) Then

                ch = CStr(ch)

                If (ch = ChrW(DL_ST)) Then
                    IsString = True
                ElseIf ((Left$(ch, 1) = ChrW(DL_ST)) And (Right$(ch, 1) = ChrW(DL_ST))) Then
                    IsString = True
                Else
                    IsString = False
                End If
            Else
                IsString = False
            End If

        End Function

        Public Function IsObjectType(code As Object) As Boolean

            If (TryCast(code, TObjects.TObject) Is Nothing) Then
                IsObjectType = False
            Else
                IsObjectType = True
            End If
        End Function


        Public Function IsBoolean(ch As Object) As Boolean

            If (Not IsObjectType(ch)) Then
                If (IsEquStr(ch, KW_TRUE) Or IsEquStr(ch, KW_FALSE)) Then
                    IsBoolean = True
                Else
                    IsBoolean = False
                End If
            Else
                IsBoolean = False
            End If
        End Function

        Public Function IsComment(ch As String) As Boolean
            If (ch = ChrW(DL_CM)) Then
                IsComment = True
            Else
                IsComment = False
            End If
        End Function

        Public Function IsFunction(CODE As String) As Boolean

            If (IsEquStr(CODE, FUNC_SIN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_COS)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_TAN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_LN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_EXP)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_DEG)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RAD)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_FLOOR)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_CEIL)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_ABS)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_FACTO)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_PI)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RND)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_NEG)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_JOIN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_LOGIC)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_NUMERIC)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_LOWERBOUND)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_UPPERBOUND)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_SUM)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_PRODUCT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MEAN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_STD)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MAX)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MIN)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RANGE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RANDOMRANGE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MATRIXMULTIPLY)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MATRIXADD)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MATRIXSUBTRACT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MATRIXDIVIDE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_TRANSPOSE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RANDOMMATRIX)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RANDOMVECTOR)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_VECTORMULTIPLY)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_VECTORADD)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_VECTORSUBTRACT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_VECTORDIVIDE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_INNERPRODUCT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_OUTERPRODUCT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_MERGE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, KW_NOT)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_RANDOM)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_TOTABLE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_TOJSON)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_JSON)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_GETJSON)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_LOADJSON)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_LOADDATASOURCE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_SAVEDATASOURCE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_SEARCHBYKEY)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_SEARCHBYKEYVALUE)) Then
                IsFunction = True
            ElseIf (IsEquStr(CODE, FUNC_INPUT)) Then
                IsFunction = True
            Else
                IsFunction = False
            End If

        End Function

        Public Function IsArray(CODE As String) As Boolean

            If (IsEquStr(CODE, KW_NUMBER_ARRAY)) Then
                IsArray = True
            ElseIf (IsEquStr(CODE, KW_OBJECT_ARRAY)) Then
                IsArray = True
            ElseIf (IsEquStr(CODE, KW_BOOLEAN_ARRAY)) Then
                IsArray = True
            ElseIf (IsEquStr(CODE, KW_STRING_ARRAY)) Then
                IsArray = True
            Else
                IsArray = False
            End If

        End Function

        Public Function IsKeyword(CODE As String) As Boolean

            If (CODE Is Nothing) Then
                IsKeyword = False
            ElseIf (KEYWORDS.Contains(CODE.ToUpper())) Then
                IsKeyword = True
            Else
                IsKeyword = False
            End If

        End Function

        Public Function IsObject(CODE As String) As Boolean

            If (IsEquStr(CODE, KW_OBJECT)) Then
                IsObject = True
            Else
                IsObject = False
            End If

        End Function

        Public Function IsProcedure(CODE As String) As Boolean

            If (IsEquStr(CODE, PROC_OUTPUT)) Then
                IsProcedure = True
            ElseIf (IsEquStr(CODE, PROC_MSGBOX)) Then
                IsProcedure = True
            ElseIf (IsEquStr(CODE, PROC_WRITE)) Then
                IsProcedure = True
            ElseIf (IsEquStr(CODE, PROC_SERIALIZE)) Then
                IsProcedure = True
            Else
                IsProcedure = False
            End If

        End Function

        Public Function IsUsrDef(CODE As String) As Boolean

            If (DF_READ(CODE) > 0) Then
                IsUsrDef = True
            Else
                IsUsrDef = False
            End If

        End Function

        Public Function IsArraySubscript(CODE As Object) As Boolean

            IsArraySubscript = False

            If (Not CODE Is Nothing) Then

                If (Not IsObjectType(CODE)) Then

                    If (Not IsString(CODE)) Then

                        CODE = CStr(CODE)

                        If (CODE.Contains(ChrW(DL_OA)) Or CODE.Contains(ChrW(DL_CA))) Then
                            IsArraySubscript = True
                        Else
                            IsArraySubscript = False
                        End If
                    Else

                        IsArraySubscript = False
                    End If

                End If
            End If

        End Function

        Public Function IsEscape(CODE As Object) As Boolean

            IsEscape = False

            If (Not IsObjectType(CODE)) Then

                If (IsEquStr(CODE, KW_ENTER)) Then
                    IsEscape = True
                ElseIf (IsEquStr(CODE, KW_SPACE)) Then
                    IsEscape = True
                ElseIf (IsEquStr(CODE, KW_TAB)) Then
                    IsEscape = True
                ElseIf (IsEquStr(CODE, KW_BR)) Then
                    IsEscape = True
                ElseIf (IsEquStr(CODE, KW_NPSP)) Then
                    IsEscape = True
                End If

            End If

            Return IsEscape

        End Function

        Public Function IsWhiteSpace(ch As String) As Boolean
            If ((ch = ChrW(WS_SP)) Or (ch = ChrW(WS_TB))) Then
                IsWhiteSpace = True
            Else
                IsWhiteSpace = False
            End If
        End Function

        Public Function IsEquStr(ByRef Str1 As String, ByRef Str2 As String) As Boolean
            If (StrComp(Str1, Str2, vbTextCompare) = 0) Then
                IsEquStr = True
            Else
                IsEquStr = False
            End If
        End Function

        Public Function RowMajorFormula(ByRef TIndices As List(Of Integer), ByRef TSize As List(Of Integer), N As Integer) As Integer

            If (TIndices.Count > 0) Then


                If (N = 1) Then
                    Return TIndices(N - 1)
                ElseIf (N > 1) Then
                    Return TIndices(N - 1) + RowMajorFormula(TIndices, TSize, N - 1) * TSize(N - 1)
                Else
                    'Error: Index out of bound
                    Return -99999
                End If
            Else
                Return -9999

            End If

        End Function

        Public Function INTERPRET(ByRef EXP As String) As Integer

            Dim Result As Integer

            If (SYS_ERR_CODE = 0) Then
                TOKENIZE(EXP)
            End If

            If (SYS_ERR_CODE = 0) Then
                LEXICAL_ANALYSIS()
            End If

            If (SYS_ERR_CODE = 0) Then
                Result = SEMANTIC_ANALYSIS()
            End If

            Return Result

        End Function

    End Module

End Namespace