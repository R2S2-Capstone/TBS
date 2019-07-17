INSERT INTO `reeceros_tbs`.`Address`(`Id`, `AddressLine`, `City`, `Province`, `Country`, `PostalCode`) SELECT `Id`, `AddressLine`, `City`, `Province`, `Country`, `PostalCode` FROM `reeceros_tbs-dev`.`Address`;
INSERT INTO `reeceros_tbs`.`Carriers`(`Id`, `UserFirebaseId`, `Name`, `Email`, `CompanyId`) SELECT `Id`, `UserFirebaseId`, `Name`, `Email`, `CompanyId` FROM `reeceros_tbs-dev`.`Carriers`;
INSERT INTO `reeceros_tbs`.`Company`(`Id`, `Name`, `AddressId`, `ContactId`) SELECT `Id`, `Name`, `AddressId`, `ContactId` FROM `reeceros_tbs-dev`.`Company`;
INSERT INTO `reeceros_tbs`.`Contact`(`Id`, `Name`, `PhoneNumber`) SELECT `Id`, `Name`, `PhoneNumber` FROM `reeceros_tbs-dev`.`Contact`;
INSERT INTO `reeceros_tbs`.`Shippers`(`Id`, `Name`, `UserFirebaseId`, `Email`, `CompanyId`) SELECT `Id`, `Name`, `UserFirebaseId`, `Email`, `CompanyId` FROM `reeceros_tbs-dev`.`Shippers`;