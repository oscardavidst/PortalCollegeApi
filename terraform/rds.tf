# Subnet group: RDS requiere 2 subnets en distintas AZ
resource "aws_db_subnet_group" "main" {
  name       = "${var.project_name}-db-subnet-group"
  subnet_ids = [aws_subnet.public_a.id, aws_subnet.public_b.id]

  tags = {
    Name        = "${var.project_name}-db-subnet-group"
    Environment = var.environment
  }
}

# RDS SQL Server Express Edition
resource "aws_db_instance" "sqlserver" {
  identifier     = "${var.project_name}-db"
  engine         = "sqlserver-ex"
  engine_version = "15.00.4415.2.v1"
  instance_class = "db.t3.small"

  username      = var.db_username
  password      = var.db_password
  license_model = "license-included"

  allocated_storage     = 20
  max_allocated_storage = 100
  storage_type          = "gp2"
  storage_encrypted     = true

  db_subnet_group_name   = aws_db_subnet_group.main.name
  vpc_security_group_ids = [aws_security_group.rds.id]

  # Acceso publico necesario para que Lambda (en subnet publica) se conecte
  publicly_accessible = true
  multi_az            = false

  # Free Tier: backup_retention_period debe ser 0
  # Cambia a 7 cuando actualices tu cuenta a una cuenta completa de AWS
  backup_retention_period = 0
  maintenance_window      = "sun:04:00-sun:05:00"

  skip_final_snapshot       = true   # Para demo: no crear snapshot al destruir
  deletion_protection       = false

  tags = {
    Name        = "${var.project_name}-sqlserver"
    Environment = var.environment
  }
}
