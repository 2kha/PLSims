Namespace PLSims

    Public Module Memory


        'This module contains the functionality to control
        'memory management and memory related operations

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'Data Type Declaration

        Public Structure T_VAR
            Public Name As String
            Public Type As String
            Public Value As Object
        End Structure

        Public Structure T_DEF
            Public ReturnType As String
            Public Name As String
            Public Args As Integer
            Public Address As Long
        End Structure

        Public Structure T_PTR_OPR
            Public OPR_BASE As Integer
            Public POX_PTR As Integer
            Public POX_STR As Integer
        End Structure

        Public Structure T_PTR_LV
            Public LV_STR As Integer
            Public LV_PTR As Integer
        End Structure
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        'Variable Declaration

        Public PRO() As String                              'Program Array

        Public DEF As New Dictionary(Of String, T_DEF)      'Procedure/Function Definition
        Public VAR As New Dictionary(Of String, T_VAR)      'Global Variable Array

        Public LVR() As T_VAR                               'Local Vairable Array

        Public OPR() As Object                              'Operand Space
        Public POX() As String                              'Postfix Space

        Public OPR_INDICES() As T_PTR_OPR                   'Operand Pointer Array
        Public LV_INDICES() As T_PTR_LV                     'Local Variable Index Array

        Public OPR_BASE As Integer                          'Operand Base of Stack Pointer
        Public OPR_PTR As Integer                           'Operand Stack Pointer

        Public POX_START As Integer                         'Postfix Start Pointer
        Public POX_END As Integer                           'Postfix End Pointer
        Public POX_PTR As Integer                           'Postfix Pointer

        Public LV_START As Integer                          'LVR Start Pointer
        Public LV_END As Integer                            'LVR End Pointer
        Public LV_PTR As Integer                            'LVR Pointer

        Public PR_INDEX As Long                             'Program Index
        Public DF_INDEX As Integer                          'Definition Index

        Public VR_INDEX As Integer                          'Variable Index


        Public Function V_READ(ByRef Name As String) As Object

            Dim GloVar As New T_VAR

            Try

                V_READ = SYS_NULL

                If (VAR.ContainsKey(Name)) Then

                    GloVar = VAR(Name)

                    V_READ = GloVar.Value

                End If

            Catch ex As Exception

                Return SYS_NULL

            End Try

        End Function

        Public Sub V_WRITE(ByRef Name As String, ByRef Value As Object, ByRef Flag As Boolean)

            Dim GloVar As New T_VAR

            Flag = False

            Try

                If (VAR.ContainsKey(Name)) Then

                    GloVar = VAR(Name)

                    GloVar.Value = Value
                    VAR(Name) = GloVar

                    Flag = True

                End If

            Catch ex As Exception

            End Try

        End Sub

        Public Sub V_ADD(ByRef Name As String, ByRef Value As Object, DataType As String)

            Dim GloVar As New T_VAR

            GloVar.Name = Name
            GloVar.Value = Value
            GloVar.Type = DataType

            If (Not VAR.ContainsKey(Name)) Then
                VAR.Add(Name, GloVar)
            End If

        End Sub

        Public Sub V_ADD(ByRef Name As String, ByRef Value As Object)

            Dim GloVar As New T_VAR

            GloVar.Name = Name
            GloVar.Value = Value
            GloVar.Type = String.Empty

            If (Not VAR.ContainsKey(Name)) Then
                VAR.Add(Name, GloVar)
            End If

        End Sub

        Public Sub LV_ADD(ByRef Name As String, ByRef Value As Object, DataType As String)

            ReDim Preserve LVR(LV_END)

            LVR(LV_END).Name = Name
            LVR(LV_END).Value = Value
            LVR(LV_END).Type = DataType


            LV_END = LV_END + 1

        End Sub

        Public Sub LV_ADD(ByRef Name As String, ByRef Value As Object)

            ReDim Preserve LVR(LV_END)

            LVR(LV_END).Name = Name
            LVR(LV_END).Value = Value
            LVR(LV_END).Type = String.Empty


            LV_END = LV_END + 1

        End Sub


        Public Function LV_W_READ(ByRef Name As String) As Object

            LV_W_READ = SYS_NULL

            Try

                Dim Index As Integer

                If (LVR Is Nothing) Then

                    Return LV_W_READ

                Else

                    For Index = LV_START To LV_END

                        If (Name = LVR(Index).Name) Then
                            LV_W_READ = LVR(Index).Value

                            Exit For
                        End If

                    Next Index

                    Return LV_W_READ

                End If


            Catch Ex As Exception

                If (Err.Number = 9) Then
                    LV_W_READ = SYS_NULL
                End If

                Return LV_W_READ

            End Try

        End Function

        Public Function LV_READ(ByRef Name As String) As Object

            LV_READ = SYS_NULL

            Try
                Dim Index As Long

                If (LV_PTR = 0) Then Exit Function

                If (LVR Is Nothing) Then

                    Return LV_READ

                Else

                    For Index = LV_PTR - 1 To LV_END - 1

                        If (Name = LVR(Index).Name) Then
                            LV_READ = LVR(Index).Value

                            Exit For
                        End If

                    Next Index

                    Return LV_READ

                End If


            Catch ex As Exception

                If (Err.Number = 9) Then

                    LV_READ = SYS_NULL

                End If

                Return LV_READ

            End Try


        End Function


        Public Sub LV_W_WRITE(ByRef Name As String, ByRef Value As Object, ByRef Flag As Boolean)

            Flag = False

            Try
                Dim Index As Long

                If (LVR Is Nothing) Then

                    Exit Sub

                Else

                    For Index = LV_START To LV_END

                        If (Name = LVR(Index).Name) Then

                            LVR(Index).Value = Value
                            Flag = True

                            Exit For
                        End If

                    Next Index

                End If

            Catch ex As Exception

                If (Err.Number = 9) Then
                    Flag = False
                End If

            End Try


        End Sub


        Public Sub LV_WRITE(ByRef Name As String, ByRef Value As Object, ByRef Flag As Boolean)


            Flag = False

            Try

                Dim Index As Long

                If (LVR Is Nothing) Then

                    Exit Sub

                Else
                    For Index = LV_PTR To LV_END

                        If (Name = LVR(Index).Name) Then

                            LVR(Index).Value = Value
                            Flag = True

                            Exit For
                        End If

                    Next Index

                    Exit Sub

                End If

            Catch ex As Exception

                If (Err.Number = 9) Then

                    Flag = False

                End If

            End Try

        End Sub

        Public Sub DF_WRITE(ByRef ReturnType As String, ByRef Name As String, ByRef Args As Integer, ByRef Address As Long)

            Dim FuncDef As New T_DEF

            If (DEF.ContainsKey(Name)) Then

                FuncDef = DEF(Name)

                FuncDef.ReturnType = ReturnType
                FuncDef.Name = Name
                FuncDef.Args = Args
                FuncDef.Address = Address

                DEF(Name) = FuncDef

            Else

                FuncDef.ReturnType = ReturnType
                FuncDef.Name = Name
                FuncDef.Args = Args
                FuncDef.Address = Address

                DEF.Add(Name, FuncDef)

            End If

        End Sub

        Public Function DF_READ(ByRef Name As String, Optional ByRef Args As Integer = 0, Optional ByRef ReturnType As String = "") As Long

            Dim FuncDef As New T_DEF

            Dim DFREAD As Long = 0

            If (DEF.ContainsKey(Name)) Then

                FuncDef = DEF(Name)

                DFREAD = FuncDef.Address
                Args = FuncDef.Args

            End If

            Return DFREAD

        End Function


        Public Function MEM_READ(Name As String) As Object

            Dim Value As Object

            Value = SYS_NULL

            Try

                If (Not (Name Is Nothing Or IsKeyword(Name))) Then

                    Value = LV_READ(Name)

                    If (Value Is Nothing) Then
                        Value = LV_W_READ(Name)

                        If (Value Is Nothing) Then
                            Value = V_READ(Name)

                            If (Value Is Nothing) Then

                                If (Not (IsUsrDef(Name) Or IsFunction(Name) Or IsNumeric(Name))) Then
                                    SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_VARIABLE
                                End If

                            End If
                        End If
                    End If
                End If

                Return Value

            Catch ex As Exception

                Return Value
            End Try

        End Function

     
        Public Sub MEM_WRITE(ByRef Name As String, ByRef Value As Object)

            Dim Flag As Boolean

            Flag = False

            Try
                If (Not (Name Is Nothing Or IsKeyword(Name))) Then

                    LV_WRITE(Name, Value, Flag)

                    If (Not Flag) Then

                        LV_W_WRITE(Name, Value, Flag)

                        If (Not Flag) Then
                            V_WRITE(Name, Value, Flag)

                            If (Not Flag) Then
                                SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_VARIABLE
                            End If

                        End If

                    End If
                End If

            Catch ex As Exception

                SYS_ERR_CODE = ErrorType.SYS_ERROR_MISSING_VARIABLE

            End Try

        End Sub

    
    End Module

End Namespace
