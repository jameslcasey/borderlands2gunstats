
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

	SELECT g.Id, g.Damage, g.Accuracy, g.FireRate, g.ReloadSpeed, g.MagazineSize, g.[Type], g.DamageOnTarget, g.ElementalDamageOnTargetTimesDamagePerSecondTimesChance, g.ElementalDamagePerSecond
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
						   ElementalDamageOnTargetTimesDamagePerSecondTimesChance decimal(18, 2) NULL, 
						   ElementalDamagePerSecond decimal(18, 2) NULL, 
						   [Type] int NULL, 
						   AllTypesDamageOnTargetRank int NULL, 
						   AllTypesElementalDamageOnTargetRank int NULL, 
						   EachElementalTypeDamageOnTargetRank int NULL, 
						   EachTypeDamageOnTargetRank int NULL
);

	INSERT INTO @AllGuns( Id, [Type], DamageOnTarget, ElementalDamageOnTargetTimesDamagePerSecondTimesChance, ElementalDamagePerSecond )
	VALUES( 999999, @Type, @DamageOnTarget, @ElementalDamageOnTarget, @ElementalDamagePerSecond );
	INSERT INTO @AllGuns( Id, DamageOnTarget, ElementalDamageOnTargetTimesDamagePerSecondTimesChance, ElementalDamagePerSecond, [Type] )
		   SELECT ag.Id, ag.DamageOnTarget, ag.ElementalDamageOnTargetTimesDamagePerSecondTimesChance, ag.ElementalDamagePerSecond, ag.[Type]
		   FROM #AllGuns AS ag;


	SELECT g.Id, ROW_NUMBER() OVER(ORDER BY g.DamageOnTarget DESC) AS rank
	INTO #AllTypeRankings
	FROM @AllGuns AS g;
	SELECT g.Id, ROW_NUMBER() OVER(PARTITION BY g.Type ORDER BY g.DamageOnTarget DESC) AS rank
	INTO #EachTypeRankings
	FROM @AllGuns AS g;
	SELECT g.Id, ROW_NUMBER() OVER(ORDER BY g.ElementalDamageOnTargetTimesDamagePerSecondTimesChance DESC) AS rank
	INTO #AllElementalTypeRankings
	FROM @AllGuns AS g
	WHERE g.ElementalDamagePerSecond > 0;
	SELECT g.Id, ROW_NUMBER() OVER(PARTITION BY g.Type ORDER BY g.ElementalDamageOnTargetTimesDamagePerSecondTimesChance DESC) AS rank
	INTO #EachElementalTypeRankings
	FROM @AllGuns AS g
	WHERE g.ElementalDamagePerSecond > 0;
	UPDATE g
	  SET g.AllTypesDamageOnTargetRank = atr.rank, g.EachTypeDamageOnTargetRank = etr.rank, g.AllTypesElementalDamageOnTargetRank = ISNULL(aetr.rank, 0), g.EachElementalTypeDamageOnTargetRank = ISNULL(eetr.rank, 0)
	FROM @AllGuns g
		 JOIN
		 #AllTypeRankings atr
		 ON g.Id = atr.Id
		 JOIN
		 #EachTypeRankings etr
		 ON g.Id = etr.Id
		 LEFT JOIN
		 #AllElementalTypeRankings aetr
		 ON g.Id = aetr.Id
		 LEFT JOIN
		 #EachElementalTypeRankings eetr
		 ON g.Id = eetr.Id;
	SELECT *
	FROM @AllGuns AS ag
	ORDER BY ag.DamageOnTarget DESC;
	DROP TABLE #AllGuns;
	DROP TABLE #AllTypeRankings;
	DROP TABLE #EachTypeRankings;
	DROP TABLE #AllElementalTypeRankings;
	DROP TABLE #EachElementalTypeRankings;
END;
GO
	
	



