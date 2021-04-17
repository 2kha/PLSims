Program


	@Global Variable
	Number a

	Procedure Local()

		@Local Variable
		Number a

		a := 30

		output("Local a: ", a, Enter)

	End Procedure



	Procedure Main()

		a := 45
		
		Local(void)

		output("Global a: ", a, Enter)
		

	End Procedure


End Program