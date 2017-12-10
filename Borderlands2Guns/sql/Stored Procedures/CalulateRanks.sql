
-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'CalculateRanks' 
)
   DROP PROCEDURE dbo.CalculateRanks
GO


-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Casey
-- Create date: 12/10/2017
-- Description:	updates the gun rank over all gun types on damageOnTarget
-- =============================================
CREATE PROCEDURE [dbo].[CalculateRanks]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	g.Id,
	ROW_NUMBER() OVER(ORDER BY g.DamageOnTarget DESC) rank
	INTO #AllTypeRankings
	FROM dbo.Guns g;

	SELECT 
	g.Id,
	ROW_NUMBER() OVER(PARTITION BY g.Type  ORDER BY g.DamageOnTarget DESC) rank
	INTO #EachTypeRankings
	FROM dbo.Guns g;

	SELECT 
	g.Id,
	ROW_NUMBER() OVER(ORDER BY g.ElementalDamageOnTargetTimesDamagePerSecondTimesChance DESC) rank
	INTO #AllElementalTypeRankings
	FROM dbo.Guns g
	WHERE g.ElementalDamagePerSecond > 0;

	SELECT 
	g.Id,
	ROW_NUMBER() OVER(PARTITION BY g.Type ORDER BY g.ElementalDamageOnTargetTimesDamagePerSecondTimesChance DESC) rank
	INTO #EachElementalTypeRankings
	FROM dbo.Guns g
	WHERE g.ElementalDamagePerSecond > 0;

	UPDATE g
	SET 
		g.AllTypesDamageOnTargetRank = atr.rank,
		g.EachTypeDamageOnTargetRank = etr.rank,
		g.AllTypesElementalDamageOnTargetRank = ISNULL(aetr.rank, 0),
		g.EachElementalTypeDamageOnTargetRank = ISNULL(eetr.rank, 0)
	FROM dbo.Guns g
	JOIN #AllTypeRankings atr ON g.Id = atr.Id
	JOIN #EachTypeRankings etr ON g.Id = etr.Id
	LEFT JOIN #AllElementalTypeRankings aetr ON g.Id = aetr.Id
	LEFT JOIN #EachElementalTypeRankings eetr ON g.Id = eetr.Id

	DROP TABLE #AllTypeRankings;
	DROP TABLE #EachTypeRankings;
	DROP TABLE #AllElementalTypeRankings;
	DROP TABLE #EachElementalTypeRankings;

END
GO
	
	



