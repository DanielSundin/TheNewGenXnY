# TheNewGenXnY

### Statens Statistiska Centralbyrås Elektroniska Undersökningsplattform
* Lägga in undersökning med frågor (Admin-läge / Admin-app)
* Välja skala frågorna skall besvaras med
* Svara på frågor (Användarläge / Användar-app)
* Kod för att accessa en viss undersökning
* Svaren måste vara anonyma, men det ska lagras att en viss användare har svarat på en viss undersökning


## TODO

* Svara på survey
* lista med färdig survey
* Dapper - Skicka in, ta bort

               if (loginauth.CheckLoginInfo(inputName, inputPass) == 1)
                {
                    System.Console.WriteLine("Username or Password was incorrect");
                    if (loginAttempts >= 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Too many attempts, try again later.");
                        Environment.Exit(0);
                    }

                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 2)
                {
                    RunUserMode(surveyManager);
                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 3)
                {

                    RunAdminMode(surveyManager);
                }


            if (loginauth.CheckLoginInfo(inputName, inputPass) == 1)
                {
                    System.Console.WriteLine("Username or Password was incorrect");
                    if (loginAttempts >= 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Too many attempts, try again later.");
                        Environment.Exit(0);
                    }

                }
                
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == true)
                {
                    if (loginauth.IsAdmin(inputName, inputPass) == true)
                    {

                        RunAdminMode(surveyManager);
                    }
                    else
                    {
                        RunUserMode(surveyManager);
                    }
                }
            }
     