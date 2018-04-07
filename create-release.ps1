dotnet publish -c Release -r win10-x64
& "C:\Program Files\7-Zip\7z.exe" a -t7z win10-x64.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\win10-x64\*.exe
& "C:\Program Files\7-Zip\7z.exe" a -t7z win10-x64.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\win10-x64\*.dll
& "C:\Program Files\7-Zip\7z.exe" a -t7z win10-x64.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\win10-x64\*.json

dotnet publish -c Release -r osx.10.11-x64
& "C:\Program Files\7-Zip\7z.exe" a -t7z osx.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\osx.10.11-x64\*.exe
& "C:\Program Files\7-Zip\7z.exe" a -t7z osx.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\osx.10.11-x64\*.dll
& "C:\Program Files\7-Zip\7z.exe" a -t7z osx.7z .\KenBonny.CheckHibp\bin\Release\netcoreapp2.0\osx.10.11-x64\*.json
