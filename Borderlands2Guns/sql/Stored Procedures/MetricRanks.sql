
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
		RANK() OVER(ORDER  BY g.Damage DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.Damage DESC) typerank
	INTO #rankings
	FROM @AllGuns g
	WHERE g.Id = @temp_id;


END
ELSE IF @metric = 'accuracy'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], Accuracy )
	VALUES( 999999, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], Accuracy )
		   SELECT ag.Id, ag.Type, ag.Accuracy 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		RANK() OVER(ORDER  BY g.Accuracy DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.Accuracy DESC) typerank
	INTO #rankings
	FROM @AllGuns AS g
	WHERE g.Id = @temp_id;

END
ELSE IF @metric = 'firerate'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], FireRate )
	VALUES( 999999, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], FireRate )
		   SELECT ag.Id, ag.Type, ag.FireRate 
		   FROM #AllGuns AS ag;

	SELECT	
		g.Id, 
		RANK() OVER(ORDER  BY g.FireRate DESC)  allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.FireRate DESC) typerank
	INTO #rankings
	FROM @AllGuns AS g
	WHERE g.Id = @temp_id;

END
ELSE IF @metric = 'reloadspeed'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], ReloadSpeed )
	VALUES( 999999, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], ReloadSpeed )
		   SELECT ag.Id, ag.Type, ag.ReloadSpeed 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		RANK() OVER(ORDER  BY g.ReloadSpeed DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.ReloadSpeed DESC) typerank
	INTO #rankings
	FROM @AllGuns AS g
	WHERE g.Id = @temp_id;

END
ELSE IF @metric = 'magazinesize'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], MagazineSize )
	VALUES( 999999, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], MagazineSize )
		   SELECT ag.Id, ag.Type, ag.MagazineSize 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		RANK() OVER(ORDER  BY g.MagazineSize DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.ReloadSpeed DESC) typerank
	INTO #rankings
	FROM @AllGuns AS g
	WHERE g.Id = @temp_id;

END
ELSE IF @metric = 'damageOnTarget'
BEGIN

	INSERT INTO @AllGuns( Id, [Type], DamageOnTarget )
	VALUES( 999999, @type, @value);
	INSERT INTO @AllGuns( Id, [Type], DamageOnTarget )
		   SELECT ag.Id, ag.Type, ag.DamageOnTarget 
		   FROM #AllGuns AS ag;

	SELECT 
		g.Id, 
		RANK() OVER(ORDER  BY g.DamageOnTarget DESC) allrank,
		RANK() OVER(PARTITION BY g.[Type] ORDER  BY g.DamageOnTarget DESC) typerank
	INTO #rankings
	FROM @AllGuns AS g
	WHERE g.Id = @temp_id;

END
	
	SELECT * FROM #rankings;

	DROP TABLE #rankings;
	DROP TABLE #AllGuns;
	DROP TABLE #AllTypeRankings;
	DROP TABLE #EachTypeRankings;
	DROP TABLE #AllElementalTypeRankings;
	DROP TABLE #EachElementalTypeRankings;
END;
GO
	
	



