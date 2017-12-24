
-- Drop stored procedure if it already exists

IF EXISTS
(
	SELECT *
	FROM INFORMATION_SCHEMA.ROUTINES
	WHERE SPECIFIC_SCHEMA = N'dbo' AND 
		  SPECIFIC_NAME = N'MetricRanks'
)
BEGIN
	DROP PROCEDURE dbo.MetricRanks;
END;
GO
-- ================================================

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO
-- =============================================
-- Author:		James Casey
-- Create date: 12/18/2017
-- Description:	calculates the metric rank over all gun types on damageOnTarget
-- =============================================

CREATE PROCEDURE dbo.MetricRanks 
				 @metric nvarchar(100), @value decimal(18, 2), @type int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @temp_id int = 9999999;

	SELECT g.Id, g.Damage, g.Accuracy, g.FireRate, g.ReloadSpeed, g.MagazineSize, g.[Type], g.DamageOnTarget
	INTO #AllGuns
	FROM dbo.Guns AS g;

	DECLARE @AllGuns TABLE
    ( 
	   Id int NULL, 
	   Damage int null,
	   Accuracy decimal(18,2) null,
	   FireRate decimal(18,2) null,
	   ReloadSpeed decimal(18,2) NULL,
	   MagazineSize int null,
	   DamageOnTarget decimal(18, 2) NULL, 
	   [Type] int NULL
    );

IF @metric = 'damage'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], Damage )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], Damage )
		   SELECT ag.Id, ag.Type, ag.Damage 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.Damage DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.Damage DESC) typerank
	INTO #rankings1
	FROM @AllGuns g;

	SELECT * FROM #rankings1 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings1;

END
ELSE IF @metric = 'accuracy'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], Accuracy )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], Accuracy )
		   SELECT ag.Id, ag.Type, ag.Accuracy 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.Accuracy DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.Accuracy DESC) typerank
	INTO #rankings2
	FROM @AllGuns g;

	SELECT * FROM #rankings2 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings2;

END
ELSE IF @metric = 'firerate'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], FireRate )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], FireRate )
		   SELECT ag.Id, ag.Type, ag.FireRate 
		   FROM #AllGuns AS ag;

	SELECT	
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.FireRate DESC)  allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.FireRate DESC) typerank
	INTO #rankings3
	FROM @AllGuns g;

	SELECT * FROM #rankings3 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings3;

END
ELSE IF @metric = 'reloadspeed'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], ReloadSpeed )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], ReloadSpeed )
		   SELECT ag.Id, ag.Type, ag.ReloadSpeed 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.ReloadSpeed ) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.ReloadSpeed ) typerank
	INTO #rankings4
	FROM @AllGuns g;

	SELECT * FROM #rankings4 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings4;

END
ELSE IF @metric = 'magazinesize'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], MagazineSize )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], MagazineSize )
		   SELECT ag.Id, ag.Type, ag.MagazineSize 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.MagazineSize DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.MagazineSize DESC) typerank
	INTO #rankings5
	FROM @AllGuns g;

	SELECT * FROM #rankings5 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings5;

END
ELSE IF @metric = 'damageOnTarget'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], DamageOnTarget )
	VALUES( @temp_id, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], DamageOnTarget )
		   SELECT ag.Id, ag.Type, ag.DamageOnTarget 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		(select count(ag.Id) from @AllGuns ag) allrankcount,
		(select count(ag.Id) from @AllGuns ag where ag.[type] = @type) typerankcount,
		RANK() OVER(ORDER  BY g.DamageOnTarget DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.DamageOnTarget DESC) typerank
	INTO #rankings6
	FROM @AllGuns g;

	SELECT * FROM #rankings6 r WHERE r.Id = @temp_id;
	DROP TABLE #rankings6;

END
	
END;
GO
	
	



