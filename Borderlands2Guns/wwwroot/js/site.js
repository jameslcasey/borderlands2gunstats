// Write your JavaScript code.

$(document).ready(function () {

    $("#mytable").DataTable();

    $("#gunname").on("keyup", function () {

        var term = $(this).val();
        $.get("GunNameSearch?ss=" + term, function (data, status) {


            var json = $.parseJSON(data);

            console.log(json);


            $("#gunSearchResults").html(data);

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