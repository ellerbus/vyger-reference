$ErrorActionPreference = "Stop"

$root = "$PSScriptRoot"
$dist = "$root\dist\vyger-app"
$docs = [System.IO.Path]::GetFullPath("$root\..\docs")

if (-not(Test-Path -Path $dist)) {
    New-Item -Path $dist -ItemType Directory | Out-Null
}

Write-Host $docs

if (Test-Path -Path $docs) {
    Remove-Item -Path "$docs\*.*"
}
else {
    New-Item -Path $docs -ItemType Directory | Out-Null
}

Write-Host "Building .. " -ForegroundColor Green
&ng build --prod --base-href ./

if (Test-Path -Path $dist) {
    Write-Host "Copying to docs .. " -ForegroundColor Yellow
    Copy-Item -Path "$dist\*.*" -Destination "$docs\"
}
else {
    Write-Error "Missing $dist"
}
