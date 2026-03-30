# ─────────────────────────────────────────────
# Connection String de RDS (SecureString, cifrada con KMS)
# Terraform la construye automáticamente con el endpoint real de RDS
# ─────────────────────────────────────────────
resource "aws_ssm_parameter" "db_connection" {
  name        = "/PortalCollegeApi/ConnectionStrings/DefaultConnection"
  description = "Connection string para RDS SQL Server"
  type        = "SecureString"
  value       = "Server=${aws_db_instance.sqlserver.address},1433;Database=PortalCollegeDb;User Id=${var.db_username};Password=${var.db_password};TrustServerCertificate=True;MultipleActiveResultSets=True"

  tags = {
    Name        = "${var.project_name}-db-connection"
    Environment = var.environment
  }
}

# ─────────────────────────────────────────────
# JWT Settings
# ─────────────────────────────────────────────
resource "aws_ssm_parameter" "jwt_key" {
  name        = "/PortalCollegeApi/JWTSettings/Key"
  description = "Clave secreta para firma de JWT"
  type        = "SecureString"
  value       = var.jwt_key

  tags = {
    Name        = "${var.project_name}-jwt-key"
    Environment = var.environment
  }
}

resource "aws_ssm_parameter" "jwt_issuer" {
  name  = "/PortalCollegeApi/JWTSettings/Issuer"
  type  = "String"
  value = "SchoolIssuer"
}

resource "aws_ssm_parameter" "jwt_audience" {
  name  = "/PortalCollegeApi/JWTSettings/Audience"
  type  = "String"
  value = "SchoolAudience"
}

resource "aws_ssm_parameter" "jwt_duration" {
  name  = "/PortalCollegeApi/JWTSettings/DurationInMinutes"
  type  = "String"
  value = "360"
}
