Program


	Procedure Main()

		Object a,b,c
		Object Array [] class

		a.Name := "Student 1"
		a.Age := 15
		a.GPA := 3.4
		a.Id := 2201		

		b.Name := "Student 2"
		b.Age := 14
		b.GPA := 3.7
		b.Id := 2202	
		
		c.Name := "Student 3"
		c.Age := 16
		c.GPA := 3.0
		c.Id := 2203	

		class[0] := a
		class[1] := b
		class[2] := c

		class[3] := Json("{Name : 'Student 4', Age : 16, GPA : 3.2, Id : 2204 }")

		Output(ToJson(class))

	End Procedure	

End Program