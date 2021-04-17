Program


	@Recursive Function
	Number Function Factorial(Number n)

		Number F
		
		If (n <= 0)
		Then
			Return 1
		Else
			F := n * Factorial(n - 1)
		End If	
	
	End Function
	

	Procedure Main()

		Number a

		a := Factorial(5)

		Output(a)


	End Procedure	

End Program