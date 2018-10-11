$ErrorActionPreference = "Stop"

$root = "$PSScriptRoot"
$dist = "$root\dist\vyger-app"
$docs = [System.IO.Path]::GetFullPath("$root\..\docs")

Write-Host $docs

if (Test-Path -Path $docs) {
    Remove-Item -Path $docs
}

New-Item -Path $docs -ItemType Directory | Out-Null

Write-Host "Building .. " -ForegroundColor Green
&ng build --prod

Write-Host "Copying to docs .. " -ForegroundColor Yellow
Copy-Item -Path "$dist\*.*" -Destination "$docs\"
