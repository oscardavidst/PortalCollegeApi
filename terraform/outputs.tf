output "api_gateway_url" {
  description = "URL base del API Gateway (usar esta en el frontend)"
  value       = "${aws_api_gateway_stage.prod.invoke_url}"
}

output "lambda_function_name" {
  description = "Nombre de la función Lambda"
  value       = aws_lambda_function.api.function_name
}

output "rds_endpoint" {
  description = "Endpoint del RDS SQL Server (solo accesible dentro de la VPC)"
  value       = aws_db_instance.sqlserver.address
  sensitive   = true
}

output "ssm_parameter_prefix" {
  description = "Prefijo de los parámetros en SSM Parameter Store"
  value       = "/PortalCollegeApi/"
}
