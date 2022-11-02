:: Creates a Variable for the Output File
@SET file="pex_test_results.txt"

:: Erases Everything Currently In the Output File
type NUL>%file%

:: ----------------------------------------
:: TITLE
:: ----------------------------------------
echo PEX TEST CASES (C1C Alex Singh) >> %file%

:: ----------------------------------------
:: GOOD EXAMPLES
:: ----------------------------------------
echo Testing Assign and Arithmetic >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assignarithmeticgood.txt >> %file%
echo. >> %file%
echo Testing Booleans >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolgood.txt >> %file%
echo. >> %file%
echo Testing Function Declaration >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\funcdeclaregood.txt >> %file%
echo. >> %file%
echo Testing If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\ifgood.txt >> %file%
echo. >> %file%
echo Testing Main >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\mainprogramgood.txt >> %file%
echo. >> %file%
echo Testing Procedure Calls >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\proceduregood.txt >> %file%
echo. >> %file%
echo Testing Variable Declaration >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\vardecgood.txt >> %file%
echo. >> %file%
echo Testing While Loops >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\whilegood.txt >> %file%
echo. >> %file%

:: ----------------------------------------
:: BAD EXAMPLES
:: ----------------------------------------
echo Running Incorrect Test Cases >> %file%
echo. >> %file%
echo Testing Incorrect Assign and Arithmetic >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assignbad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assignbad2.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\assignbad3.txt >> %file%
echo. >> %file%
echo Testing Incorrect Booleans >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolbad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolbad2.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\boolbad3.txt >> %file%
echo. >> %file%
echo Testing Incorrect Function Declarations >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\funcdeclarebad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\funcdeclarebad2.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\funcdeclarebad3.txt >> %file%
echo. >> %file%
echo Testing Incorrect If Statements >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\ifbad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\ifbad2.txt >> %file%
echo. >> %file%
echo Testing Incorrect Procedure Calls >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\procedurebad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\procedurebad2.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\procedurebad3.txt >> %file%
echo. >> %file%
echo Testing Incorrect Variable Declaration >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\vardecbad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\vardecbad2.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\vardecbad3.txt >> %file%
echo. >> %file%
echo Testing Incorrect While Loops >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\whilebad1.txt >> %file%
echo. >> %file%
bin\Debug\ConsoleApplication.exe testcases\pex2\whilebad2.txt >> %file%
echo. >> %file%
pause