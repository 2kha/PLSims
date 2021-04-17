Program

	Procedure Main()

		@Number Array
		Number Array[][] a

		Number x,y

		While (x < 10)

			y := 0

			While (y < 10)
			
				a[x][y] := x + y

				y := y + 1

			End While 

		
			x := x + 1

		End While

		Output(ToJson(a))

	End Procedure

End Program