CREATE TABLE `t_label` (
	`L_Id` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_0900_ai_ci',
	`L_Name` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_0900_ai_ci',
	`L_Pa` VARCHAR(20) NOT NULL COLLATE 'utf8mb4_0900_ai_ci',
	`L_Lass` INT(10) NOT NULL DEFAULT '0',
	PRIMARY KEY (`L_Id`) USING BTREE
)
COLLATE='utf8mb4_0900_ai_ci'
ENGINE=INNODB
