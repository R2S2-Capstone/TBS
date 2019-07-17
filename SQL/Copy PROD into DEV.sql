INSERT INTO `reeceros_tbs-dev`.`Address`(`Id`, `AddressLine`, `City`, `Province`, `Country`, `PostalCode`) SELECT `Id`, `AddressLine`, `City`, `Province`, `Country`, `PostalCode` FROM `reeceros_tbs`.`Address`;
INSERT INTO `reeceros_tbs-dev`.`Carriers`(`Id`, `UserFirebaseId`, `Name`, `Email`, `CompanyId`) SELECT `Id`, `UserFirebaseId`, `Name`, `Email`, `CompanyId` FROM `reeceros_tbs`.`Carriers`;
INSERT INTO `reeceros_tbs-dev`.`Company`(`Id`, `Name`, `AddressId`, `ContactId`) SELECT `Id`, `Name`, `AddressId`, `ContactId` FROM `reeceros_tbs`.`Company`;
INSERT INTO `reeceros_tbs-dev`.`Contact`(`Id`, `Name`, `PhoneNumber`) SELECT `Id`, `Name`, `PhoneNumber` FROM `reeceros_tbs`.`Contact`;
INSERT INTO `reeceros_tbs-dev`.`Shippers`(`Id`, `Name`, `UserFirebaseId`, `Email`, `CompanyId`) SELECT `Id`, `Name`, `UserFirebaseId`, `Email`, `CompanyId` FROM `reeceros_tbs`.`Shippers`;