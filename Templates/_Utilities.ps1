$class_name = $table.name | Pascal
$class_names = $class_name | Pluralize

$collection = $class_name + "Collection"
$repository = "I" + $class_name + "Repository"
$validator = $class_name + "Validator"
$ivalidator = "IValidator<" + $class_name + ">"
$service = $class_name + "Service"
$controller = $class_names + "Controller"
$builder = $class_name + "Builder"
$table_type = $class_name + "TableType"
$api = "api/$class_names" | Lower

if ($config.Namespace -ilike '*Tests') {
    $common_ns = $config.Namespace -replace 'Tests', 'Common'
}
elseif ($config.Namespace -ilike '*Web') {
    $common_ns = $config.Namespace -replace 'Web$', 'Common'
}

$web_ns = $common_ns -replace 'Common$', 'Web'

$var_name = $class_name | Last | Lower
$var_names = $class_name | Last | Lower | Pluralize

#   all columns
$columns = $table.Columns 

#   primary key columns
$primary_keys = $table.Columns | Where-Object { $_.IsPrimaryKey }

#   unique key columns
$unique_keys = $table.Columns | Where-Object { $_.IsUniqueKey }

#   foreign key columns
$foreign_keys = $table.Columns | Where-Object { $_.IsForeignKey }

#   insertable columns
$insert_columns = $table.Columns | Where-Object { -not ($_.IsAutoNumber -or $_.IsComputed) }

#   updatable columns
$update_columns = $table.Columns | Where-Object { -not ($_.IsPrimaryKey -or $_.IsAutoNumber -or $_.IsComputed) }