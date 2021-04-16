Namespace PLSims


    Public Module Evaluation


        'This module contain the functionality to control
        'evaluation of expressions, functions and procedures

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'Constant Declaration

        'Mathematical Constants
        Public Const PI = 3.14159265358979
        Public Const EX = 2.71828182845905

        'Mathematical Functions
        Public Const FUNC_SIN = "SIN"
        Public Const FUNC_COS = "COS"
        Public Const FUNC_TAN = "TAN"

        Public Const FUNC_LN = "LN"
        Public Const FUNC_EXP = "EXP"

        Public Const FUNC_DEG = "DEG"
        Public Const FUNC_RAD = "RAD"

        Public Const FUNC_FLOOR = "FLOOR"
        Public Const FUNC_CEIL = "CEIL"
        Public Const FUNC_ABS = "ABS"

        Public Const FUNC_FACTO = "FACTO"

        Public Const FUNC_PI = "PI"
        Public Const FUNC_RND = "RND"
        Public Const FUNC_NEG = "NEG"

        'Conversion Functions
        Public Const FUNC_LOGIC = "LOGIC"
        Public Const FUNC_NUMERIC = "NUMERIC"
        Public Const FUNC_STR = "STR"

        'Matrix Functions       
        Public Const FUNC_RANDOMMATRIX = "RANDOMMATRIX"
        Public Const FUNC_TRANSPOSE = "TRANSPOSE"
        Public Const FUNC_MATRIXMULTIPLY = "MATRIXMULTIPLY"
        Public Const FUNC_MATRIXADD = "MATRIXADD"
        Public Const FUNC_MATRIXSUBTRACT = "MATRIXSUBTRACT"
        Public Const FUNC_MATRIXDIVIDE = "MATRIXDIVIDE"

        'Vector Functions
        Public Const FUNC_RANDOMVECTOR = "RANDOMVECTOR"
        Public Const FUNC_INNERPRODUCT = "INNERPRODUCT"
        Public Const FUNC_OUTERPRODUCT = "OUTPRODUCT"
        Public Const FUNC_VECTORMULTIPLY = "VECTORMULTIPLY"
        Public Const FUNC_VECTORADD = "VECTORXADD"
        Public Const FUNC_VECTORSUBTRACT = "VECTORSUBTRACT"
        Public Const FUNC_VECTORDIVIDE = "VECTORDIVIDE"



        'Set Funcitons
        Public Const FUNC_UNION = "UNION"
        Public Const FUNC_INTERSECT = "INTERSECT"
        Public Const FUNC_MERGE = "MERGE"

        'Size Functions
        Public Const FUNC_LOWERBOUND = "LBOUND"
        Public Const FUNC_UPPERBOUND = "UBOUND"

        'Search Functions
        Public Const FUNC_SEARCHBYKEY = "SEARCHBYKEY"
        Public Const FUNC_SEARCHBYKEYVALUE = "SEARCH"
        Public Const FUNC_FILTER = "FILTER"


        'JSON Functions
        Public Const FUNC_JSON = "JSON"
        Public Const FUNC_GETJSON = "GETJSON"
        Public Const FUNC_LOADJSON = "LOADJSON"

        'Datasource Functions
        Public Const FUNC_LOADDATASOURCE = "LOADDATASOURCE"
        Public Const FUNC_SAVEDATASOURCE = "SAVEDATASOURCE"


        'Output Format Functions
        Public Const FUNC_TOJSON = "TOJSON"
        Public Const FUNC_TOCSV = "TOCSV"
        Public Const FUNC_TOXML = "TOXML"

        '
        Public Const FUNC_TOTABLE = "TOTABLE"

        'Aggregation Functions
        Public Const FUNC_SUM = "SUM"
        Public Const FUNC_PRODUCT = "PRODUCT"
        Public Const FUNC_MEAN = "MEAN"
        Public Const FUNC_MAX = "MAX"
        Public Const FUNC_MIN = "MIN"

        'Conversion String Functions
        Public Const FUNC_JOIN = "JOIN"

        'Stastical Functions
        Public Const FUNC_RANDOM = "RANDOM"
        Public Const FUNC_RANGE = "RANGE"
        Public Const FUNC_RANDOMRANGE = "RANDOMRANGE"
        Public Const FUNC_STD = "STD"
        Public Const FUNC_VARIANCE = "VARIANCE"
        Public Const FUNC_COVARIANCE = "COVARIANCE"

        'IO Functions
        Public Const FUNC_INPUT = "INPUT"
        Public Const PROC_OUTPUT = "OUTPUT"
        Public Const PROC_MSGBOX = "MSGBOX"
        Public Const PROC_WRITE = "WRITE"

        'Serialization Functions      
        Public Const PROC_SERIALIZE = "SERIALIZE"

        'Output String
        Public OUTPUT_STRING As String = String.Empty
        Public OUTPUT_JSON As String = String.Empty
        Public OUTPUT_ELAPSEDTIME As Single


        Public Sub EVALUATE(ByRef Result As Object, Optional ByRef NX_ADDR As Long = 0, Optional BEG_INDEX As Integer = 0)

            Dim Index As Integer

            Dim CODE As String

            Dim A As Object
            Dim B As Object

            Dim S As Object

            Dim C As Object
            Dim D As Object

            Dim X As Double
            Dim Y As Double

            Dim T As Boolean
            Dim F As Boolean

            Try

                If (SYS_ERR_CODE = 0) Then

                    If (BEG_INDEX > 0) Then
                        Index = BEG_INDEX
                    Else
                        Index = POX_START
                        OPR_PTR = OPR_BASE - 1
                    End If

                    While (Index <= POX_END)

                        CODE = POX(Index)

                        'Dynamic Object Accessor. 

                        If IsResolver(CODE) Then

                            A = OPR(OPR_PTR)

                            OPR_PTR = OPR_PTR - 1

                            B = OPR(OPR_PTR)

                            If (Not IsObjectType(B)) Then
                                D = MEM_READ(B)
                            Else
                                D = B
                            End If

                            ReDim Preserve OPR(OPR_PTR)

                            OPR(OPR_PTR) = D(A)

                        ElseIf IsArraySubscript(CODE) Then

                            A = OPR(OPR_PTR)

                            Dim ArrayIndices = Indices(CODE)

                            If (Not IsObjectType(A)) Then
                                C = MEM_READ(A)
                            Else
                                C = A
                            End If

                            ReDim Preserve OPR(OPR_PTR)

                            OPR(OPR_PTR) = C.SearchByIndices(ArrayIndices)

                        ElseIf IsArithmetic(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                A = OPR(OPR_PTR)
                                OPR_PTR = OPR_PTR - 1

                                If (IsObjectType(A)) Then
                                    X = Val(A.Data)

                                ElseIf (Not IsNumeric(A)) Then

                                    C = MEM_READ(A)

                                    If (IsObjectType(C)) Then
                                        X = Val(C.Data)
                                    Else
                                        X = Val(C)
                                    End If

                                Else
                                    X = Val(A)
                                End If

                                    ReDim Preserve OPR(OPR_PTR)

                                    B = OPR(OPR_PTR)

                                    If (IsObjectType(B)) Then
                                        Y = Val(B.Data)
                                    ElseIf (Not IsNumeric(B)) Then
                                    D = MEM_READ(B)

                                    If (IsObjectType(D)) Then
                                        Y = Val(D.Data)
                                    Else
                                        Y = Val(D)
                                    End If
                                    Else
                                        Y = Val(B)
                                    End If

                                    OPR(OPR_PTR) = CStr(EVAL_ARITHMETIC(CODE, Y, X))

                            End If

                        ElseIf IsRelational(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                A = OPR(OPR_PTR)
                                OPR_PTR = OPR_PTR - 1

                                If (IsObjectType(A)) Then
                                    X = Val(A.Data)
                                ElseIf (Not IsNumeric(A)) Then
                                    C = MEM_READ(A)

                                    If (IsObjectType(C)) Then
                                        X = Val(C.Data)
                                    Else
                                        X = Val(C)
                                    End If

                                Else
                                    X = Val(A)
                                End If

                                ReDim Preserve OPR(OPR_PTR)

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    Y = Val(B.Data)
                                ElseIf (Not IsNumeric(B)) Then
                                    D = MEM_READ(B)

                                    If (IsObjectType(D)) Then
                                        Y = Val(D.Data)
                                    Else
                                        Y = Val(D)
                                    End If
                                Else
                                    Y = Val(B)
                                End If

                                OPR(OPR_PTR) = CStr(EVAL_RELATIONAL(CODE, Y, X))

                            End If

                        ElseIf IsLogical(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                A = OPR(OPR_PTR)
                                OPR_PTR = OPR_PTR - 1

                                If (IsObjectType(A)) Then
                                    T = CBool(A.Data)
                                ElseIf (Not IsBoolean(A)) Then
                                    C = MEM_READ(A)

                                    If (IsObjectType(C)) Then
                                        T = CBool(C.Data)
                                    Else
                                        T = CBool(C)
                                    End If


                                Else
                                    T = CBool(A)
                                End If

                                ReDim Preserve OPR(OPR_PTR)

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    F = Val(B.Data)
                                ElseIf (Not IsBoolean(B)) Then
                                    D = MEM_READ(B)

                                    If (IsObjectType(D)) Then
                                        F = CBool(D.Data)
                                    Else
                                        F = Val(D)
                                    End If
                                    F = CBool(D)
                                Else
                                    F = CBool(B)
                                End If

                                OPR(OPR_PTR) = CStr(EVAL_LOGICAL(CODE, F, T))

                            End If

                        ElseIf IsUnary(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    Y = Val(B.Data)
                                ElseIf (Not IsNumeric(B)) Then
                                    D = MEM_READ(B)

                                    If (IsObjectType(D)) Then
                                        Y = Val(D.Data)
                                    Else
                                        Y = Val(D)
                                    End If

                                    Y = Val(D)
                                Else
                                    Y = Val(B)
                                End If

                                OPR(OPR_PTR) = CStr(-Y)

                            End If

                        ElseIf IsAssignment(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                A = OPR(OPR_PTR)
                                OPR_PTR = OPR_PTR - 1

                                If (IsObjectType(A)) Then
                                    'Do Nothing
                                ElseIf ((IsNumeric(A) Or (IsBoolean(A)) Or (IsString(A)) Or (IsEscape(A)))) Then
                                    'Do Nothing
                                Else
                                    C = MEM_READ(A)
                                End If


                                ReDim Preserve OPR(OPR_PTR)

                                B = OPR(OPR_PTR)

                                If IsObjectType(B) Then

                                    If ((IsNumeric(A) Or (IsBoolean(A)) Or (IsString(A)) Or (IsEscape(A)))) Then
                                        B.Data = A
                                        OPR(OPR_PTR) = B

                                    ElseIf (IsObjectType(A)) Then

                                        B.Set(A)
                                        OPR(OPR_PTR) = B

                                    ElseIf ((IsNumeric(C) Or (IsBoolean(C)) Or (IsString(C)) Or (IsEscape(C)))) Then
                                        B.Data = C
                                        OPR(OPR_PTR) = B

                                    ElseIf (IsObjectType(C)) Then

                                        B.Set(C)
                                        OPR(OPR_PTR) = B

                                    End If

                                Else

                                    If ((IsNumeric(A) Or (IsBoolean(A)) Or (IsString(A)) Or (IsEscape(A)))) Then
                                        MEM_WRITE(B, A)
                                        OPR(OPR_PTR) = A

                                    ElseIf IsObjectType(A) Then

                                        MEM_WRITE(B, A)
                                        OPR(OPR_PTR) = A


                                    ElseIf IsObjectType(C) Then

                                        MEM_WRITE(B, C)
                                        OPR(OPR_PTR) = C

                                    ElseIf ((IsNumeric(C) Or (IsBoolean(C)) Or (IsString(C)) Or (IsEscape(C)))) Then

                                        MEM_WRITE(B, C)
                                        OPR(OPR_PTR) = C

                                    End If
                                End If


                            End If

                        ElseIf IsSeperator(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                A = OPR(OPR_PTR)
                                OPR_PTR = OPR_PTR - 1

                                If (IsObjectType(A)) Then
                                    'Do Nothing
                                ElseIf ((IsNumeric(A) Or (IsBoolean(A)) Or (IsString(A)) Or (IsEscape(A)))) Then
                                    'Do Nothing
                                Else
                                    C = MEM_READ(A)
                                End If

                                ReDim Preserve OPR(OPR_PTR)

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    'Do Nothing
                                ElseIf ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B)))) Then
                                    'Do Nothing
                                Else
                                    D = MEM_READ(B)
                                End If


                                If (Not B Is SYS_NULL) Then

                                    If ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B))) Or _
                                        IsObjectType(B)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = B


                                    ElseIf ((IsNumeric(D) Or (IsBoolean(D)) Or (IsString(D)) Or (IsEscape(D))) Or _
                                         IsObjectType(D)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = D
                                    End If

                                End If

                                If (Not A Is SYS_NULL) Then
                                    If ((IsNumeric(A) Or (IsBoolean(A)) Or (IsString(A)) Or (IsEscape(A))) Or _
                                         IsObjectType(A)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = A


                                    ElseIf ((IsNumeric(C) Or (IsBoolean(C)) Or (IsString(C)) Or (IsEscape(C))) Or _
                                         IsObjectType(C)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = C

                                    End If
                                End If

                                OPR(OPR_PTR) = SYS_NULL
                            End If

                        ElseIf IsFunction(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    'Do Nothing
                                ElseIf (IsNumeric(B) Or IsBoolean(B) Or IsString(B) Or IsEscape(B)) Then
                                    'Do Nothing
                                Else
                                    D = MEM_READ(B)
                                End If

                                If (Not B Is SYS_NULL) Then

                                    If ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B))) Or _
                                        IsObjectType(B)) Then

                                        OPR(OPR_PTR) = EVAL_FUNCTION(CODE, B)

                                    ElseIf ((IsNumeric(D) Or (IsBoolean(D)) Or (IsString(D)) Or (IsEscape(D))) Or _
                                         IsObjectType(D)) Then

                                        OPR(OPR_PTR) = EVAL_FUNCTION(CODE, D)
                                    End If
                                Else
                                    OPR(OPR_PTR) = EVAL_FUNCTION(CODE, B)
                                End If

                            End If

                        ElseIf IsProcedure(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    'Do Nothing
                                ElseIf (IsNumeric(B) Or IsBoolean(B) Or IsString(B) Or IsEscape(B)) Then
                                    'Do Nothing
                                Else
                                    D = MEM_READ(B)
                                End If

                                If (Not B Is SYS_NULL) Then

                                    If ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B))) Or _
                                        IsObjectType(B)) Then

                                        EVAL_PROCEDURE(CODE, B)
                                        OPR(OPR_PTR) = SYS_NULL

                                    ElseIf ((IsNumeric(D) Or (IsBoolean(D)) Or (IsString(D)) Or (IsEscape(D))) Or _
                                         IsObjectType(D)) Then

                                        EVAL_PROCEDURE(CODE, D)
                                        OPR(OPR_PTR) = SYS_NULL

                                    End If
                                Else
                                    EVAL_PROCEDURE(CODE, B)
                                    OPR(OPR_PTR) = SYS_NULL
                                End If

                            End If

                        ElseIf IsUsrDef(CODE) Then

                            If (OPR_PTR <> (OPR_BASE - 1)) Then

                                B = OPR(OPR_PTR)

                                If (IsObjectType(B)) Then
                                    'Do Nothing
                                ElseIf ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B)))) Then
                                    'Do Nothing
                                Else
                                    D = MEM_READ(B)
                                End If

                                If (Not B Is SYS_NULL) Then

                                    If ((IsNumeric(B) Or (IsBoolean(B)) Or (IsString(B)) Or (IsEscape(B))) Or _
                                        IsObjectType(B)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = B


                                    ElseIf ((IsNumeric(D) Or (IsBoolean(D)) Or (IsString(D)) Or (IsEscape(D))) Or _
                                         IsObjectType(D)) Then

                                        SYS_ARG_INDEX = SYS_ARG_INDEX + 1
                                        ReDim Preserve SYS_ARGS(SYS_ARG_INDEX)

                                        SYS_ARGS(SYS_ARG_INDEX) = D
                                    End If

                                End If

                                OPR(OPR_PTR) = SYS_NULL
                                POX_PTR = Index + 1
                                Result = SYS_NULL
                                NX_ADDR = DF_READ(CODE)

                                Exit Sub

                            End If

                        Else
                            OPR_PTR = OPR_PTR + 1
                            ReDim Preserve OPR(OPR_PTR)

                            OPR(OPR_PTR) = CODE

                        End If

                        Index = Index + 1

                    End While

                    If (OPR_PTR <> 0) Then

                        B = OPR(OPR_PTR)

                        If (IsObjectType(B)) Then

                            Result = B

                        Else

                            D = MEM_READ(B)

                            If (Not D Is SYS_NULL) Then
                                Result = D
                            Else
                                Result = B
                            End If

                        End If

                    End If

                End If

            Catch ex As Exception
                SYS_ERR_CODE = ErrorType.SYS_ERROR_RUNTIME_EVALUATION_FAIL
            End Try

        End Sub

        Private Function FACTORIAL(NO As Double) As Double

            Dim fact As Double

            Dim NUM As Integer
            Dim i As Integer

            NUM = NO

            If (NUM < 0) Then
            Else
                fact = 1

                For i = 1 To NUM
                    fact = fact * i
                Next
            End If

            Return fact

        End Function

        Private Function EVAL_ARITHMETIC(Optor As String, X As Double, Y As Double) As Double

            Dim result As Double

            Select Case Optor

                Case ChrW(DL_MU)
                    result = X * Y

                Case ChrW(DL_PL)
                    result = X + Y

                Case ChrW(DL_MI)
                    result = X - Y

                Case ChrW(DL_DI)
                    If Not (Y = 0) Then
                        result = X / Y
                    End If

                Case ChrW(DL_MD)
                    result = X Mod Y

                Case ChrW(DL_PW)
                    result = X ^ Y

            End Select

            Return result

        End Function

        Private Function EVAL_RELATIONAL(Optor As String, X As Double, Y As Double) As Boolean

            Dim relational As Boolean

            If (Optor = ChrW(DL_LT)) Then
                relational = (X < Y)

            ElseIf (Optor = ChrW(DL_GT)) Then
                relational = (X > Y)

            ElseIf (Optor = ChrW(DL_EQ)) Then
                relational = (X = Y)

            ElseIf (Optor = (ChrW(DL_GT) + ChrW(DL_EQ))) Then
                relational = (X >= Y)

            ElseIf (Optor = (ChrW(DL_LT) + ChrW(DL_EQ))) Then
                relational = (X <= Y)

            ElseIf (Optor = (ChrW(DL_LT) + ChrW(DL_GT))) Then
                relational = (X <> Y)
            End If

            Return relational

        End Function

        Private Function EVAL_LOGICAL(Optor As String, X As Boolean, Y As Boolean) As Boolean

            Dim logical As Boolean

            If (IsEquStr(Optor, KW_AND)) Then
                logical = (X And Y)

            ElseIf (IsEquStr(Optor, KW_OR)) Then
                logical = (X Or Y)

            ElseIf (IsEquStr(Optor, KW_XOR)) Then
                logical = (X Xor Y)
            End If

            Return logical

        End Function

        Private Function EVAL_FUNCTION(Func As String, Exps As Object) As Object

            Dim Result As Object = Nothing
            Dim Index As Integer
            Dim ST As String
            Dim STS As String


            If (IsEquStr(Func, FUNC_SIN)) Then
                Result = Math.Sin(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_COS)) Then
                Result = Math.Cos(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_TAN)) Then
                Result = Math.Tan(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_LN)) Then
                Result = Math.Log(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_EXP)) Then
                Result = Math.Exp(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_DEG)) Then

            ElseIf (IsEquStr(Func, FUNC_RAD)) Then

            ElseIf (IsEquStr(Func, FUNC_SEARCHBYKEY)) Then


                If (Not (Exps Is Nothing)) Then
                    Result = SYS_NULL
                Else

                    Dim Key As String = ""
                    Dim TObj As TObjects.TObject = Nothing

                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            Key = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            TObj = SYS_ARGS(Index)

                        End If

                    Next Index

                    SYS_ARG_INDEX = 0

                    If (Not (TObj Is Nothing)) Then
                        Result = TObj.SearchByKey(Key)
                    Else
                        Result = SYS_NULL
                    End If


                End If

            ElseIf (IsEquStr(Func, FUNC_SEARCHBYKEYVALUE)) Then


                If (Not (Exps Is Nothing)) Then
                    Result = SYS_NULL
                Else

                    Dim Value As New List(Of Object)
                    Dim TObj As TObjects.TObject = Nothing

                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            Value.Add(REMOVE_IVC(SYS_ARGS(Index)))
                        ElseIf (IsNumeric(SYS_ARGS(Index))) Then
                            Value.Add(SYS_ARGS(Index))
                        ElseIf (IsBoolean(SYS_ARGS(Index))) Then
                            Value.Add(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            TObj = SYS_ARGS(Index)

                        End If

                    Next Index

                    SYS_ARG_INDEX = 0

                    If (Not (TObj Is Nothing)) Then
                        If (Value.Count > 1) Then
                            Result = TObj.SearchByKeyValue(Value(0).ToString(), Value(1))
                        Else
                            Result = SYS_NULL
                        End If

                    Else
                        Result = SYS_NULL
                    End If


                End If

            ElseIf (IsEquStr(Func, FUNC_TOTABLE)) Then

                Dim tObj As TObjects.TObject

                tObj = TryCast(Exps, TObjects.TObject)

                If (Exps Is Nothing) Then
                    Result = Nothing

                ElseIf (IsObjectType(Exps)) Then
                    Result = tObj.ToTable()
                End If

            ElseIf (IsEquStr(Func, FUNC_TOJSON)) Then


                If (Exps Is Nothing) Then
                    Result = ChrW(DL_ST) + ChrW(DL_OA) + ChrW(DL_CA) + ChrW(DL_ST)


                ElseIf (IsObjectType(Exps)) Then
                    Result = ChrW(DL_ST) + Exps.ToJSON() + ChrW(DL_ST)
                End If

            ElseIf (IsEquStr(Func, FUNC_JSON)) Then

                If (IsString(Exps)) Then

                    Result = TObjects.TObject.JSON(REMOVE_RT(REMOVE_IVC(Exps)))

                End If

            ElseIf (IsEquStr(Func, FUNC_GETJSON)) Then

                If (IsString(Exps)) Then

                    Result = TObjects.TFramework.GetJSON(REMOVE_IVC(Exps))

                End If

            ElseIf (IsEquStr(Func, FUNC_LOADJSON)) Then

                If (IsString(Exps)) Then

                    Result = TObjects.TFramework.LoadJSON(REMOVE_IVC(Exps))

                End If

            ElseIf (IsEquStr(Func, FUNC_LOADDATASOURCE)) Then

                If (IsString(Exps)) Then

                    Result = TApplication.TDataSource.LoadDatasource(REMOVE_IVC(Exps))

                End If

            ElseIf (IsEquStr(Func, FUNC_SAVEDATASOURCE)) Then

                Dim data As New TObjects.TObject
                Dim para As New TObjects.TObject

                Dim dsName As String = ""

                Try

                    For Index = 1 To SYS_ARG_INDEX

                        If (IsObjectType(SYS_ARGS(Index))) Then

                            data = SYS_ARGS(Index)

                        ElseIf IsString(SYS_ARGS(Index)) Then

                            dsName = REMOVE_IVC(SYS_ARGS(Index))

                        End If

                    Next Index

                    SYS_ARG_INDEX = 0

                    para("Name").Data = dsName
                    para("Type").Data = 6
                    para("Settings").Data = "{}"

                    TApplication.TDataSource.SaveDatasource(para, data)

                Catch ex As Exception

                    Result = False
                End Try

                Result = True



            ElseIf (IsEquStr(Func, FUNC_RANDOM)) Then

                Dim Rnd As New Random(Guid.NewGuid().GetHashCode())

                Result = Rnd.NextDouble() * Convert.ToDouble(Exps)

            ElseIf (IsEquStr(Func, FUNC_RANGE)) Then

                Dim parameters As New List(Of Object)

                For Index = 1 To SYS_ARG_INDEX
                    parameters.Add(SYS_ARGS(Index))
                Next Index

                SYS_ARG_INDEX = 0

                Result = TObjects.TMath.Range(parameters(0), parameters(1), parameters(2))

            ElseIf (IsEquStr(Func, FUNC_RANDOMRANGE)) Then

                Dim parameters As New List(Of Object)

                For Index = 1 To SYS_ARG_INDEX
                    parameters.Add(SYS_ARGS(Index))
                Next Index

                SYS_ARG_INDEX = 0

                Result = TObjects.TMath.RandomRange(parameters(0), parameters(1))

            ElseIf (IsEquStr(Func, FUNC_RANDOMMATRIX)) Then

                Dim parameters As New List(Of Object)

                For Index = 1 To SYS_ARG_INDEX
                    parameters.Add(SYS_ARGS(Index))
                Next Index

                SYS_ARG_INDEX = 0

                Result = TObjects.TMath.MatrixRandom(parameters(0), parameters(1))

            ElseIf (IsEquStr(Func, FUNC_RANDOMVECTOR)) Then

                Dim parameters As New List(Of Object)

                For Index = 1 To SYS_ARG_INDEX
                    parameters.Add(SYS_ARGS(Index))
                Next Index

                SYS_ARG_INDEX = 0

                Result = TObjects.TMath.VectorRandom(parameters(0))

            ElseIf (IsEquStr(Func, FUNC_LOWERBOUND)) Then

                If (IsObjectType(Exps)) Then

                    If (Exps Is Nothing) Then
                        Result = -1
                    Else
                        Result = 0
                    End If

                Else
                    Result = -1
                End If

            ElseIf (IsEquStr(Func, FUNC_UPPERBOUND)) Then

                If (IsObjectType(Exps)) Then

                    If (Exps Is Nothing) Then
                        Result = 0
                    Else

                        Dim tObj As TObjects.TObject

                        tObj = TryCast(Exps, TObjects.TObject)

                        Result = tObj.Count

                    End If

                Else
                    Result = -1
                End If

            ElseIf (IsEquStr(Func, FUNC_SUM)) Then

                If (IsObjectType(Exps)) Then
                    Result = Exps.Sum()
                Else
                    Result = 0
                End If

            ElseIf (IsEquStr(Func, FUNC_PRODUCT)) Then

                If (IsObjectType(Exps)) Then
                    Result = Exps.Product()
                Else
                    Result = 0
                End If

            ElseIf (IsEquStr(Func, FUNC_MEAN)) Then

                If (IsObjectType(Exps)) Then
                    Result = TObjects.TMath.Average(Exps).Data
                Else
                    Result = 0
                End If

            ElseIf (IsEquStr(Func, FUNC_STD)) Then

                If (IsObjectType(Exps)) Then
                    Result = TObjects.TMath.StandardDeviation(Exps).Data
                Else
                    Result = 0
                End If

            ElseIf (IsEquStr(Func, FUNC_FLOOR)) Then
                Result = Math.Floor(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_CEIL)) Then
                Result = Math.Ceiling(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_ABS)) Then
                Result = Math.Sin(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_FACTO)) Then
                Result = Math.Abs(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_PI)) Then
                Result = Math.PI.ToString()

            ElseIf (IsEquStr(Func, FUNC_RND)) Then
                Result = Math.Round(Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_NEG)) Then
                Result = (-Convert.ToDouble(Exps))

            ElseIf (IsEquStr(Func, FUNC_VECTORMULTIPLY)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 0) Then
                    Result = TObjects.TMath.VectorMultiplication(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_VECTORDIVIDE)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 0) Then
                    Result = TObjects.TMath.VectorDivision(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_VECTORSUBTRACT)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.VectorSubtraction(parameters(0), parameters(1))
                Else
                    Result = TObjects.TMath.VectorSubtraction(parameters(0), para)
                End If


            ElseIf (IsEquStr(Func, FUNC_VECTORADD)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.VectorAddition(parameters(0), parameters(1))
                Else
                    Result = TObjects.TMath.VectorAddition(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_INNERPRODUCT)) Then

                Dim parameters As New List(Of TObjects.TObject)

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))

                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.InnerProduct(parameters(0), parameters(1))
                End If

            ElseIf (IsEquStr(Func, FUNC_OUTERPRODUCT)) Then

                Dim parameters As New List(Of TObjects.TObject)

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))

                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.OuterProduct(parameters(0), parameters(1))
                End If

            ElseIf (IsEquStr(Func, FUNC_TRANSPOSE)) Then

                If (IsObjectType(Exps)) Then

                    If (Exps Is Nothing) Then
                        Result = 0
                    Else

                        Dim tObj As TObjects.TObject

                        tObj = TryCast(Exps, TObjects.TObject)

                        Result = TObjects.TMath.MatrixTranspose(tObj)

                    End If

                Else
                    Result = Nothing

                End If

            ElseIf (IsEquStr(Func, FUNC_MATRIXDIVIDE)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 0) Then
                    Result = TObjects.TMath.MatrixDivision(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_MATRIXADD)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.MatrixAddition(parameters(0), parameters(1))
                Else
                    Result = TObjects.TMath.MatrixAddition(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_MATRIXSUBTRACT)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.MatrixSubtraction(parameters(0), parameters(1))
                Else
                    Result = TObjects.TMath.MatrixSubtraction(parameters(0), para)
                End If

            ElseIf (IsEquStr(Func, FUNC_MATRIXMULTIPLY)) Then

                Dim parameters As New List(Of TObjects.TObject)
                Dim para As New Object()

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then

                        parameters.Add(SYS_ARGS(Index))
                    Else
                        para = SYS_ARGS(Index)
                    End If

                Next Index

                SYS_ARG_INDEX = 0

                If (parameters.Count > 1) Then
                    Result = TObjects.TMath.MatrixMultiplication(parameters(0), parameters(1))
                Else
                    Result = TObjects.TMath.MatrixMultiplication(parameters(0), para)
                End If


            ElseIf (IsEquStr(Func, FUNC_INPUT)) Then

                If (IsString(Exps)) Then
                    Console.Write(REMOVE_IVC(Exps))

                    Dim input As String = Console.ReadLine()

                    If (IsNumeric(input) Or IsBoolean(input)) Then
                        Result = input
                    Else

                        Result = ChrW(DL_ST) + input + ChrW(DL_ST)
                    End If
                End If

            ElseIf (IsEquStr(Func, FUNC_MERGE)) Then

                Dim parameters As New List(Of TObjects.TObject)

                For Index = 1 To SYS_ARG_INDEX

                    If (IsObjectType(SYS_ARGS(Index))) Then
                        parameters.Add(SYS_ARGS(Index))
                    End If


                    Result = TObjects.TOperations.Merge(parameters.ToArray())

                    SYS_ARG_INDEX = 0

                Next

            ElseIf (IsEquStr(Func, FUNC_JOIN)) Then

                If (Not Exps Is SYS_NULL) Then

                    If (IsString(Exps)) Then
                        ST = REMOVE_IVC(Exps)
                    ElseIf (IsEscape(Exps)) Then
                        ST = GET_ESCAPE(Exps)
                    Else
                        ST = Exps
                    End If

                    Result = ST
                Else
                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            STS = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsEscape(SYS_ARGS(Index))) Then
                            STS = GET_ESCAPE(SYS_ARGS(Index))
                        Else
                            STS = SYS_ARGS(Index)
                        End If

                        ST = ST + STS

                    Next Index

                    SYS_ARG_INDEX = 0

                    Result = ChrW(DL_ST) + ST + ChrW(DL_ST)
                End If

            ElseIf (IsEquStr(Func, FUNC_LOGIC)) Then
                Dim Num As Integer = Convert.ToInt32(Exps)

                If (Num > 0) Then
                    Result = KW_TRUE
                Else
                    Result = KW_FALSE
                End If

            ElseIf (IsEquStr(Func, FUNC_NUMERIC)) Then

            ElseIf (IsEquStr(Func, KW_NOT)) Then
                Result = (Not Convert.ToBoolean(Exps))

            Else

            End If

            Return Result

        End Function

        Private Sub EVAL_PROCEDURE(Proc As String, ByRef Exps As Object)

            Dim Index As Integer
            Dim ST As String
            Dim STS As String

            If (IsEquStr(Proc, PROC_OUTPUT)) Then

                If (Not Exps Is SYS_NULL) Then

                    If (IsString(Exps)) Then
                        ST = REMOVE_IVC(Exps)
                    ElseIf (IsEscape(Exps)) Then
                        ST = GET_ESCAPE(Exps)
                    ElseIf (IsObjectType(Exps)) Then
                        If (IsString(Exps.Data)) Then
                            ST = REMOVE_IVC(Exps.Data)
                        Else
                            ST = Exps.data
                        End If
                    Else
                        ST = Exps

                    End If

                    Console.Write(ST)
                Else
                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            STS = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsEscape(SYS_ARGS(Index))) Then
                            STS = GET_ESCAPE(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            If (IsString(SYS_ARGS(Index).Data)) Then
                                STS = REMOVE_IVC(SYS_ARGS(Index).Data)
                            Else
                                STS = SYS_ARGS(Index).data
                            End If
                        Else
                            STS = SYS_ARGS(Index)
                        End If

                        ST = ST + STS

                    Next Index

                    SYS_ARG_INDEX = 0

                    System.Console.Write(ST)
                End If

            ElseIf (IsEquStr(Proc, PROC_WRITE)) Then

                If (Not Exps Is SYS_NULL) Then

                    If (IsString(Exps)) Then
                        ST = REMOVE_IVC(Exps)
                    ElseIf (IsEscape(Exps)) Then
                        ST = GET_ESCAPE(Exps)
                    ElseIf (IsObjectType(Exps)) Then
                        If (IsString(Exps.Data)) Then
                            ST = REMOVE_IVC(Exps.Data)
                        Else
                            ST = Exps.data
                        End If
                    Else
                        ST = Exps
                    End If

                    OUTPUT_STRING += ST
                Else
                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            STS = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsEscape(SYS_ARGS(Index))) Then
                            STS = GET_ESCAPE(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            If (IsString(SYS_ARGS(Index).Data)) Then
                                STS = REMOVE_IVC(SYS_ARGS(Index).Data)
                            Else
                                STS = SYS_ARGS(Index).data
                            End If
                        Else
                            STS = SYS_ARGS(Index)
                        End If

                        ST = ST + STS

                    Next Index

                    SYS_ARG_INDEX = 0


                    OUTPUT_STRING += ST

                End If

           

            ElseIf (IsEquStr(Proc, PROC_SERIALIZE)) Then

                OUTPUT_JSON = ""

                If (Not Exps Is SYS_NULL) Then

                    If (IsString(Exps)) Then
                        ST = REMOVE_IVC(Exps)
                    ElseIf (IsObjectType(Exps)) Then
                        ST = Exps.ToJSON()
                    End If

                    OUTPUT_JSON += ST
                Else
                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            STS = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            STS = SYS_ARGS(Index).ToJSON()
                        End If

                        ST = ST + STS

                    Next Index

                    SYS_ARG_INDEX = 0


                    OUTPUT_JSON += ST

                End If
            ElseIf (IsEquStr(Proc, PROC_MSGBOX)) Then

                If (Not Exps Is SYS_NULL) Then

                    If (IsString(Exps)) Then
                        ST = REMOVE_IVC(Exps)
                    ElseIf (IsObjectType(Exps)) Then
                        If (IsString(Exps.Data)) Then
                            ST = REMOVE_IVC(Exps.Data)
                        Else
                            ST = Exps.data
                        End If
                    Else
                        ST = Exps
                    End If

                    MsgBox(ST)
                Else
                    For Index = 1 To SYS_ARG_INDEX

                        If (IsString(SYS_ARGS(Index))) Then
                            STS = REMOVE_IVC(SYS_ARGS(Index))
                        ElseIf (IsObjectType(SYS_ARGS(Index))) Then
                            If (IsString(SYS_ARGS(Index).Data)) Then
                                STS = REMOVE_IVC(SYS_ARGS(Index).Data)
                            Else
                                STS = SYS_ARGS(Index).data
                            End If
                        Else
                            STS = SYS_ARGS(Index)
                        End If

                        ST = ST + STS

                    Next Index

                    SYS_ARG_INDEX = 0

                    MsgBox(ST)
                End If
            Else

            End If


        End Sub

    End Module

End Namespace