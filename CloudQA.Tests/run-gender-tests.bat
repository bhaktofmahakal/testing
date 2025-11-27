@echo off
REM Run only Gender field tests
echo ========================================
echo Running Gender Field Tests
echo ========================================
dotnet test --filter "Category=Gender" --settings .runsettings --verbosity normal
pause
