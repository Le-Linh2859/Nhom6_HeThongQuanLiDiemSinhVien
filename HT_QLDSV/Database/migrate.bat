@echo off
REM ============================================================================
REM Script chạy migration cho DB_QLDiemSinhVien
REM Cách dùng: migrate.bat SERVERNAME
REM Ví dụ:     migrate.bat ADMIN-PC\QUYNHANH
REM ============================================================================

IF "%~1"=="" (
    echo.
    echo [LOI] Vui long truyen ten Server.
    echo Cach dung: migrate.bat TEN_SERVER
    echo Vi du:     migrate.bat ADMIN-PC\QUYNHANH
    echo.
    exit /b 1
)

SET SERVER=%~1
SET DATABASE=DB_QLDiemSinhVien

echo.
echo ============================================
echo  MIGRATION - %DATABASE%
echo  Server: %SERVER%
echo ============================================
echo.

echo [1/2] Chay V001__InitialSchema.sql ...
sqlcmd -S %SERVER% -d %DATABASE% -E -i "Migrations\V001__InitialSchema.sql"
IF %ERRORLEVEL% NEQ 0 (
    echo [LOI] V001 that bai!
    exit /b 1
)

echo.
echo [2/2] Chay V002__AddMigrationTracking.sql ...
sqlcmd -S %SERVER% -d %DATABASE% -E -i "Migrations\V002__AddMigrationTracking.sql"
IF %ERRORLEVEL% NEQ 0 (
    echo [LOI] V002 that bai!
    exit /b 1
)

echo.
echo ============================================
echo  HOAN TAT! Tat ca migration da chay xong.
echo ============================================
echo.
