// Write your JavaScript code.

$(document).ready(function () {

    var resultsDiv = $("#searchResults");

    $("#gunname").on("keyup", function () {

        resultsDiv.empty();

        var term = $(this).val();

        $.get("GunNameSearch?ss=" + term, function (data, status) {
            var json = $.parseJSON(data);
            console.log(json);

            for (var i = 0; i < json.length; i++) {
                resultsDiv.append("<div class='row'> <div class='col-md-1'>" + json[i].Level + "</div> <div class='col-md-6'>" + json[i].Name + "</div> <div class='col-md-2'>" + json[i].DamageOnTarget + "</div> </div>");
            }

        });
    });


    $("#damage, #accuracy,  #firerate, #reloadspeed, #magazinesize, #elementalDamagePerSecond, #chance").on("keyup", function () {

        var damage = $("#damage").val();
        var accuracy = $("#accuracy").val();
        var firerate = $("#firerate").val();
        var reloadspeed = $("#reloadspeed").val();
        var magazinesize = $("#magazinesize").val();
        var elementalDamagePerSecond = $("#elementalDamagePerSecond").val();
        var chance = $("#chance").val();

        $.get("GunCalcs?damage=" + damage + "&accuracy=" + accuracy + "&firerate=" + firerate + "&reloadspeed=" + reloadspeed + "&magazinesize=" + magazinesize + "&elementalDamagePerSecond=" + elementalDamagePerSecond + "&chance=" + chance, function (data, status) {

            var json = $.parseJSON(data);
            $("#damageTimesFireRate").val(json["DamageTimesFireRate"]);
            $("#damageTimesFireRateTimesMagazineSize").val(json["DamageTimesFireRateTimesMagazineSize"]);
            $("#damageTimesFireRateTimesMagazineSizePerReloadSpeed").val(json["DamageTimesFireRateTimesMagazineSizePerReloadSpeed"]);
            $("#damageOnTarget").val(json["DamageOnTarget"]);
            $("#elementalDamagePerSecondTimesChance").val(json["ElementalDamagePerSecondTimesChance"]);
            $("#elementalDamageOnTargetTimesDamagePerSecondTimesChance").val(json["ElementalDamageOnTargetTimesDamagePerSecondTimesChance"]);


        });

    });





});