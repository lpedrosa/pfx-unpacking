# lint project and fail on warnings
# current fsharplint tool doesn't have a flag to fail on any warnings
$SolutionFile = ".\PfxUnpacking.sln"

dotnet tool restore
dotnet dotnet-fsharplint -sol $SolutionFile | Tee-Object -Variable linterOutput

$matches = $linterOutput | Select-String -Pattern "Summary: (\d+) warnings"

if (-not $matches.Matches.Success) 
{
    Write-Warning "linter failed with errors (check output)"
    exit 1
}

$noWarnings = $matches.Matches.Groups[1].Value

if ($noWarnings -ne 0)
{
    Write-Warning "linter reported warnings"
    exit 1
}

Write-Host "linter was successful"
