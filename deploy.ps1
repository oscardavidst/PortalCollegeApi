<#
.SYNOPSIS
    Script de despliegue de PortalCollegeApi en AWS Lambda via Terraform.

.PARAMETER Action
    deploy   : Empaqueta + terraform apply (default)
    package  : Solo empaqueta el zip de Lambda
    infra    : Solo aplica Terraform (el zip debe existir)
    destroy  : Destruye toda la infraestructura
    output   : Muestra los outputs de Terraform

.EXAMPLE
    .\deploy.ps1
    .\deploy.ps1 -Action package
    .\deploy.ps1 -Action deploy
    .\deploy.ps1 -Action destroy
#>

param(
    [ValidateSet("deploy", "package", "infra", "destroy", "output")]
    [string]$Action = "deploy"
)

$ErrorActionPreference = "Stop"
$WebApiDir    = Join-Path $PSScriptRoot "WebApi"
$TerraformDir = Join-Path $PSScriptRoot "terraform"
$ZipPath      = Join-Path $WebApiDir "bin\Release\net8.0\WebApi.zip"

function Write-Step($msg) {
    Write-Host ""
    Write-Host "--- $msg ---" -ForegroundColor Cyan
}

function Assert-Tool($name, $command) {
    if (-not (Get-Command $command -ErrorAction SilentlyContinue)) {
        Write-Error "'$name' no esta instalado o no esta en el PATH."
        exit 1
    }
}

# Verificar herramientas necesarias
Write-Step "Verificando herramientas"
Assert-Tool "dotnet"        "dotnet"
Assert-Tool "dotnet lambda" "dotnet-lambda"
Assert-Tool "terraform"     "terraform"
Assert-Tool "AWS CLI"       "aws"
Write-Host "OK - Todas las herramientas estan disponibles"

# Verificar credenciales AWS
Write-Step "Verificando credenciales AWS"
$identityJson = aws sts get-caller-identity --output json 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Error "Las credenciales de AWS no estan configuradas. Ejecuta: aws configure"
    exit 1
}
$identity = $identityJson | ConvertFrom-Json
Write-Host "OK - Conectado como: $($identity.Arn)"

# Verificar terraform.tfvars
$tfvarsPath = Join-Path $TerraformDir "terraform.tfvars"
if ($Action -ne "output" -and -not (Test-Path $tfvarsPath)) {
    Write-Error "No existe '$tfvarsPath'. Copia 'terraform.tfvars.example' a 'terraform.tfvars' y completa los valores."
    exit 1
}

# ---------------------------------------------------------------
# Funcion: Empaquetar Lambda zip
# ---------------------------------------------------------------
function Invoke-Package {
    Write-Step "Empaquetando Lambda zip"
    Push-Location $WebApiDir
    try {
        dotnet lambda package --configuration Release --framework net8.0
        if ($LASTEXITCODE -ne 0) { throw "dotnet lambda package fallo" }
        Write-Host "OK - Zip generado en: $ZipPath"
    }
    finally {
        Pop-Location
    }
}

# ---------------------------------------------------------------
# Funcion: Aplicar Terraform
# ---------------------------------------------------------------
function Invoke-Infra {
    Write-Step "Inicializando Terraform"
    Push-Location $TerraformDir
    try {
        terraform init
        if ($LASTEXITCODE -ne 0) { throw "terraform init fallo" }

        Write-Step "Planificando cambios"
        terraform plan -out=tfplan
        if ($LASTEXITCODE -ne 0) { throw "terraform plan fallo" }

        Write-Step "Aplicando infraestructura en AWS"
        terraform apply tfplan
        if ($LASTEXITCODE -ne 0) { throw "terraform apply fallo" }

        Write-Step "Outputs"
        terraform output
    }
    finally {
        Pop-Location
    }
}

# ---------------------------------------------------------------
# Funcion: Destruir infraestructura
# ---------------------------------------------------------------
function Invoke-Destroy {
    Write-Host ""
    Write-Host "ADVERTENCIA: Esto destruira TODA la infraestructura (Lambda, RDS, VPC...)" -ForegroundColor Yellow
    $confirm = Read-Host "Escribe DESTRUIR para confirmar"
    if ($confirm -ne "DESTRUIR") {
        Write-Host "Cancelado."
        return
    }
    Push-Location $TerraformDir
    try {
        terraform destroy
    }
    finally {
        Pop-Location
    }
}

# ---------------------------------------------------------------
# Despachador de acciones
# ---------------------------------------------------------------
switch ($Action) {
    "package" {
        Invoke-Package
    }
    "infra" {
        if (-not (Test-Path $ZipPath)) {
            Write-Error "No existe el zip en '$ZipPath'. Ejecuta primero: .\deploy.ps1 -Action package"
            exit 1
        }
        Invoke-Infra
    }
    "deploy" {
        Invoke-Package
        Invoke-Infra
        Write-Host ""
        Write-Host "Despliegue completado exitosamente!" -ForegroundColor Green
        Write-Host ""
        Write-Host "PROXIMO PASO - Ejecutar migraciones de EF Core:" -ForegroundColor Yellow
        Write-Host "  Para correr las migraciones contra RDS necesitas acceso a la VPC privada."
        Write-Host "  Opciones:"
        Write-Host "  1. AWS Systems Manager Session Manager con port forwarding"
        Write-Host "  2. Agregar tu IP temporalmente al security group de RDS"
        Write-Host "  3. Correr las migraciones desde una EC2 dentro de la misma VPC"
    }
    "output" {
        Push-Location $TerraformDir
        try {
            terraform output
        }
        finally {
            Pop-Location
        }
    }
    "destroy" {
        Invoke-Destroy
    }
}
