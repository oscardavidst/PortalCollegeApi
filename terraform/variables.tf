variable "aws_region" {
  description = "Región AWS donde se despliegan los recursos"
  type        = string
  default     = "us-east-1"
}

variable "project_name" {
  description = "Prefijo usado en el nombre de todos los recursos"
  type        = string
  default     = "portal-college"
}

variable "environment" {
  description = "Nombre del ambiente (production, staging, etc.)"
  type        = string
  default     = "production"
}

variable "db_username" {
  description = "Usuario maestro de RDS SQL Server"
  type        = string
  default     = "adminportal"
}

variable "db_password" {
  description = "Contraseña maestra de RDS SQL Server (mínimo 8 caracteres)"
  type        = string
  sensitive   = true
}

variable "jwt_key" {
  description = "Clave secreta para firmar tokens JWT (mínimo 32 caracteres)"
  type        = string
  sensitive   = true
}

variable "lambda_zip_path" {
  description = "Ruta al zip de despliegue de Lambda (generado por dotnet lambda package)"
  type        = string
  default     = "../WebApi/bin/Release/net8.0/WebApi.zip"
}
