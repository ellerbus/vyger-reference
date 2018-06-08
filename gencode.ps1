Param(
    [Parameter(Mandatory = $true)]
    [string]
    $Template,        
    [Parameter(Mandatory = $true)]
    [string]
    $Table,        
    [string]
    $Project,        
    [switch]
    $Force = $false
)

if ($Project -and $Project -inotcontains "vyger.") {
    $Project = "vyger.$Project"
}

$root = Split-Path $DTE.Solution.Properties["Path"].Value -Parent

$data = Join-Path -Path $root -ChildPath "vyger.Web\App_Data"

$cfg = Join-Path -Path $root -ChildPath "vyger.Web\App_Data\Configs\connectionStrings.config"

$xml = [xml](Get-Content $cfg)

$connectionString = $xml.SelectSingleNode("//connectionStrings/add[@name='DB.local']/@connectionString").Value

$connectionString = $connectionString -ireplace "\|DataDirectory\|", $data

Invoke-psakusei -ConnectionString $connectionString -Template $Template -Table $Table -Force:$Force -Project $Project -Verbose
