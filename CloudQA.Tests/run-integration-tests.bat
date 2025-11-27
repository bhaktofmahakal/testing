@echo off
REM Run only integration tests
echo ========================================
echo Running Integration Tests
echo ========================================
dotnet test --filter "Category=Integration" --settings .runsettings --verbosity normal
pause
