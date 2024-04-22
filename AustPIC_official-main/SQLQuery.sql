CREATE TABLE CssVariables (
    VarName VARCHAR(50) PRIMARY KEY,
    VarValue VARCHAR(50) NOT NULL
);

INSERT INTO CssVariables (VarName, VarValue) VALUES
    ('--a-hover-color', '#73c5eb'),
    ('--navbar-scroll-color-rgb', '40, 58, 90'),
    ('--hero-bg-color', '#37517e'),
    ('--hero-button-bg-color', '#47b2e4'),
    ('--hero-button-bg-color-hover', '#209dd8'),
    ('--hero-bottom-divider-bg-color', '#f3f5fa'),
    ('--achievements-bg-img', 'url("../img/formation.jpg")'),
    ('--hero-bg-color-rgb', '55, 81, 126'),
    ('--team-divider-color', '#cbd6e9'),
    ('--team-social-bg-color', '#eff2f8'),
    ('--contact-info-icon-bg-color', '#e7f5fb'),
    ('--contact-info-p-color', '#6182ba'),
    ('--breadcrumbs-slash-color', '#4668a2');


USE [AustPIC]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************************************************************************************************
* Database:                     AustPIC
* Procedure Name:               [dbo].[AustPIC_GetAllCssVariables]
* Date:                         May 06, 2023
* Author:                       Azmain 
* Procedure Description:        Get all Css Variables
* Example:						SELECT [dbo].[AustPIC_GetAllCssVariables]
*****************************************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------
DATE:				Developer				DEN #				Change
--------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------*/
CREATE PROCEDURE [dbo].[AustPIC_GetAllCssVariables]	
AS
DECLARE @ErrNo int
BEGIN
	SET NOCOUNT ON;
	
	BEGIN
		SELECT VarName, VarValue FROM CssVariables WITH(NOLOCK)
	END
	
	SET @ErrNo = @@ERROR
    IF @ErrNo <> 0 GOTO on_error

    SET NOCOUNT OFF;
    RETURN(1)

    on_error:
    PRINT 'An error occurred and the error number is : ' + CAST(@ErrNo AS varchar(16))
    SET NOCOUNT OFF;
    RETURN(-1)
END

DROP TABLE NewsletterSubscribers
CREATE TABLE NewsletterSubscribers (
  id INT IDENTITY(1,1) PRIMARY KEY,
  email VARCHAR(255) NOT NULL UNIQUE,
  subscribed_at DATETIME DEFAULT GETDATE()
);

USE [AustPIC]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************************************************************************************************************
* Database:                     AustPIC
* Procedure Name:               [dbo].[AustPIC_SaveEmail]
* Date:                         May 13, 2023
* Author:                       Azmain 
* Procedure Description:        Save Newsletter Email
* Example:						SELECT [dbo].[AustPIC_SaveEmail]
*****************************************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------
DATE:				Developer				DEN #				Change
--------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------------------*/
CREATE PROCEDURE [dbo].[AustPIC_SaveEmail]	
(
	@email varchar(255) ,
	@subscribed_at datetime
)
AS
DECLARE @ErrNo int
BEGIN
	SET NOCOUNT ON;

	Insert INTO NewsletterSubscribers(email,
					subscribed_at)
			Values(
					@email,
					@subscribed_at
				)
	
	SET @ErrNo = @@ERROR
    IF @ErrNo <> 0 GOTO on_error

    SET NOCOUNT OFF;
    RETURN(1)

    on_error:
    PRINT 'An error occurred and the error number is : ' + CAST(@ErrNo AS varchar(16))
    SET NOCOUNT OFF;
    RETURN(-1)
END