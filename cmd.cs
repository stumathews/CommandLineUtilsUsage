 internal class Program
  {
      public static int Main(string[] args)
      {
          var app = new CommandLineApplication();
          app.Command("Command1", target =>
          {
              target.Name = "Command1";
              target.Description = "Command1 Description";
              target.OnExecute(() =>
              {
                  Console.WriteLine("Command1 not ready yet");
                  return -1;
              });
              target.HelpOption("-?|-h|--help");
          });
          app.Command("Command2", target =>
          {
              target.Name = "Command2";
              target.Description = "Command2 Description";
              target.OnExecute(() =>
              {
                  //work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          app.Command("Command3", target =>
          {
              var argPortfolioNameOrCode = target.Argument("Portfolio name/code", "The portfolio name or code");
              var argPortfolioType = target.Argument("Portfolio type", "The portfolio type");
              
              target.Description = "Command3 Description";
              target.OnExecute(() =>
              {
                  if (MissingMandatoryArguments(target, argPortfolioNameOrCode, argPortfolioType))
                  {
                      return -1;
                  }
                  //work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          
          app.Command("Command4", target =>
          {
              var argPortType = target.Argument("Portfolio Type", "The type of portfolio to get");
              target.Name = "Command4";
              target.Description = "Command4 Description";
              target.OnExecute(() =>
              {
                  if (MissingMandatoryArguments(target, argPortType))
                  {
                      return -1;
                  }
                  //work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          app.Command("Command5", target =>
          {
              target.Name = "Command5";
              target.Description = "Command5 Description";
              var argPortfolioCode = target.Argument("Code", "Code of the portfolio", false);
              var argPortfolioName = target.Argument("Portfolio Name", "Code of the portfolio", false);
              var argIsFish = target.Argument("IsFish", "Value indicating if this is a Fish owned portfolio", false);
              var argScope = target.Argument("Scope", "Fish Scope", false);
              
              target.OnExecute(() =>
              {
                  var argPortfolioCodeValue = argPortfolioCode.Value;
                  var argPortfolioNameValue = argPortfolioName.Value;
                  var argIsFishValue = argIsFish.Value;
                  var argScopeValue = argScope.Value;
                  var appCode = LambdaCore.FishPWClient.AppCode;
                  if (MissingMandatoryArguments(target, argPortfolioCode, argPortfolioName, argIsFish, argScope))
                  {
                      return -1;
                  }
                  //work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          
          app.Command("Command6", target =>
          {
              target.Name = "Command6 description";
              target.Description = "Gets the first --max-items or 5 portfolios";
              var optMax = target.Option("--max-items", "Max number of items to retrieve", CommandOptionType.SingleValue);
              target.OnExecute(() =>
              {
                  //work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          app.Command("Command7", target =>
          {
              target.Name = "Command7";
              target.Description = "Command7 description";
              var argPortfolioCode = target.Option("--code|-c", "portfolio code", CommandOptionType.SingleValue);
              var optPortfolioName = target.Option("--name|-n", "portfolio name", CommandOptionType.SingleValue);
              var optPortfolioAttributes = target.Option("--attributes", "Attributes to fetch for portfolio", CommandOptionType.MultipleValue);              
              var argPortfolioType = target.Argument("Portfolio Type", "Type of portfolio header to check (eg. WatchList)");
              
              
              target.OnExecute(() =>
              {
                  if (MissingMandatoryArguments(target, argPortfolioType))
                  {
                      return -1;
                  }
                  // work
                  return 0;
              });
              target.HelpOption("-?|-h|--help");
          });
          app.Name = "App.exe";
          app.Description = "Application";
          app.HelpOption("-?|-h|--help");
          app.OnExecute(() =>
          {
              if (args.Length != 0) return 0;
              app.ShowHelp();
              return -1;
          });
          try
          {
              var result = app.Execute(args);
              if (!Debugger.IsAttached) return result;
              Console.ReadKey();
              return result;
          }
          catch (Exception e)
          {
              Console.WriteLine(e.Message);
              if (!Debugger.IsAttached)
              {
                  Console.ReadKey();
              }
              return 1;
          }
      }
      private static bool MissingMandatoryArguments(CommandLineApplication target, params CommandArgument[] arguments)
      {
          var emptyArgs = arguments.Where(x => string.IsNullOrEmpty(x.Value)).ToArray();
          if (emptyArgs.Any())
          {
              var missingArgList = string.Join(",", emptyArgs.Select(x => x.Name));
              Console.WriteLine($"** Missing mandatory arguments: {missingArgList}");
              target.ShowHelp(target.Name);
              return true;
          }
          return false;
      }
  }
}