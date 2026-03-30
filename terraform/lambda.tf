resource "aws_lambda_function" "api" {
  function_name = "PortalCollegeApi"
  description   = "Portal College REST API — ASP.NET Core 8"

  # El zip se genera con: dotnet lambda package (desde WebApi/)
  filename         = var.lambda_zip_path
  source_code_hash = filebase64sha256(var.lambda_zip_path)

  # Con AddAWSLambdaHosting el handler es el nombre del proyecto
  handler = "WebApi"
  runtime = "dotnet8"

  role        = aws_iam_role.lambda_exec.arn
  timeout     = 30
  memory_size = 512

  environment {
    variables = {
      ASPNETCORE_ENVIRONMENT = "Production"
    }
  }

  # Lambda queda dentro de la VPC para acceder a RDS en subnets privadas
  vpc_config {
    subnet_ids         = [aws_subnet.public_a.id, aws_subnet.public_b.id]
    security_group_ids = [aws_security_group.lambda.id]
  }

  tags = {
    Name        = "${var.project_name}-function"
    Environment = var.environment
  }

  depends_on = [
    aws_iam_role_policy_attachment.lambda_logs,
    aws_iam_role_policy_attachment.lambda_vpc,
  ]
}
