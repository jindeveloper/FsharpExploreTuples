namespace FsharpExploreTuples.Test

open Xunit.Abstractions

module Tests =
    open System
    open Xunit
    open Microsoft.FSharp.Reflection

    type TupleTest (output:ITestOutputHelper) = 

 
        [<Fact>]
        let ``Create Instance of Tuple With No Parentheses`` () =
        
            (* separate group of values with commas, and optionally place them within parentheses*)
            
            (* let's start with no parentheses *)
            let food = "burger", "chicken" 

            Assert.True(FSharpType.IsTuple(food.GetType()))
            Assert.Equal(typeof<Tuple<string, string>>, food.GetType())

        [<Fact>]
        let ``Create Instance of Tuple With Parentheses`` () = 

            (* separate group of values with commas, and optionally place them within parentheses*)

            (* let's have another example with parentheses *)
            let cars = ("Toyota", "Mazda")

            Assert.True(FSharpType.IsTuple(cars.GetType()))
            Assert.Equal(typeof<Tuple<string,string>>, cars.GetType())

        [<Fact>]
        let ``Create Instance of Tuple Using Tuple.Create`` () = 
    
            let food  = Tuple.Create("food", "chicken")
            let food2 = Tuple.Create<string,string>("food", "chicken")
            let food3 = Tuple.Create<_,_>("food", "chicken")

            Assert.True(FSharpType.IsTuple(food.GetType()))

            Assert.True(food.GetType() = food2.GetType())
            Assert.True(food.GetType() = food3.GetType())
            Assert.True(food2.GetType() = food3.GetType())

        [<Fact>]
        let ``Extract Two Elements From Tuple`` () = 
    
            let numberOfCPU = ("CPU", 5)

            Assert.True(FSharpType.IsTuple(numberOfCPU.GetType()))
            Assert.True(typeof<Tuple<string, int>>.Equals(numberOfCPU.GetType()))
            Assert.Equal(typeof<Tuple<string, int>>, numberOfCPU.GetType())

            Assert.Equal(fst numberOfCPU, "CPU")
            Assert.Equal(snd numberOfCPU, 5)

         
        [<Fact>]
        let ``Extract Multiple Identifiers Seperated By Commas`` () = 
    
            let movies = ("Avengers", "Xmen", "Wolverine", "Gambit")

            let movie1, movie2, movie3, movie4 = movies 

            Assert.Equal(movie1, "Avengers")
            Assert.Equal(movie2, "Xmen")
            Assert.Equal(movie3, "Wolverine")
            Assert.Equal(movie4, "Gambit")
     
        [<Fact>]
        let ``Tuples and Pattern Matching``() = 
            
            let CheckIfAdoboExist (food: string*string*int) : bool = 
                FSharpValue.GetTupleFields(food) 
                    |> Array.contains "Adobo"  
                                                                    
            let yourFavoriteFood (foodTuple: Option<string * string * int>) :string =
                match foodTuple with 
                    | None -> "no favorite food"
                    | Some v -> match  CheckIfAdoboExist(v) with 
                                | true -> "Nice one you like adobo"
                                | false -> "I can't guess your food"
                    
            // _ indicates a wildcard pattern (*) meaning any 

            let result = yourFavoriteFood (Some ("Adobo", "", 1))
            Assert.Equal("Nice one you like adobo", result)

            let result1 = yourFavoriteFood (Some ("", "Adobo", 1))
            Assert.Equal("Nice one you like adobo", result1)

            let result2= yourFavoriteFood (Some("", "", 0))
            Assert.Equal("I can't guess your food", result2)

            let result3 = yourFavoriteFood Unchecked.defaultof<_> (* passing null *)
            Assert.Equal("no favorite food", result3)
            
          

   


