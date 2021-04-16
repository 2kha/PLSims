
Namespace PLSims

    'This module contains the functionality to debug 
    'and trace the process of interpretation

    '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


    Public Module Debugging

        Public Enum ErrorType
            SYS_ERROR_STRING_NOT_FOUND = 1
            SYS_ERROR_MISSING_BRACKET = 2
            SYS_ERROR_MISSING_PROGRAM_STRUCT = 3
            SYS_ERROR_MISSING_ANGLE_BRACKET = 4
            SYS_ERROR_RUNTIME_EVALUATION_FAIL = 5
            SYS_ERROR_RUNTIME_STACK_OVERFLOW = 6
            SYS_ERROR_MISSING_VARIABLE = 7
        End Enum

        Public ERROR_MSG(7) As String

        Public Sub LOAD_ERROR_MESSAGES()

            ERROR_MSG(1) = "Missing String"
            ERROR_MSG(2) = "Missing Bracket"
            ERROR_MSG(3) = "Missing Program Structure"
            ERROR_MSG(4) = "Missing Angle Bracket"
            ERROR_MSG(5) = "Runtime Program Evaluation fails"
            ERROR_MSG(6) = "Runtime Timeout"
            ERROR_MSG(7) = "Missing Variable(s)"

        End Sub


    End Module


End Namespace
