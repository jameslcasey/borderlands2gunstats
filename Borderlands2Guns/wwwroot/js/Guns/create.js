// JavaScript source code
$(document).ready(function () {

    //create.begin...

    $("#damage").on("blur", function () {

        var content = $(this).val();
        var numbers = content.split("*");
        var numbers_length = numbers.length;
        var result = 1;

        if (numbers_length > 1) {
            for (var i = 0; i < numbers_length; i++) {
                result *= numbers[i];
            }
            $(this).val(result);
        }
        else {
            $(this).val(content);
        }
    });

    var resultsDiv = $("#searchResults");

    $("#gunname").on("keyup", function () {

        resultsDiv.empty();

        var term = $(this).val();

        $.get("GunNameSearch?ss=" + term, function (data, status) {
            var json = $.parseJSON(data);
            for (var i = 0; i < json.length; i++) {
                resultsDiv.append("<div class='column'>" + json[i].Level + "</div> <div class='column'>" + json[i].Name + "</div> <div class='column'>" + json[i].DamageOnTarget + "</div> <div class='column'>" + json[i].AllTypesDamageOnTargetRank + "</div>");
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

        if (damage.length > 0 & accuracy.length > 0 & firerate.length > 0 & reloadspeed.length > 0 & magazinesize.length > 0) {
            $.get("/Guns/GunCalcs?damage=" + damage + "&accuracy=" + accuracy + "&firerate=" + firerate + "&reloadspeed=" + reloadspeed + "&magazinesize=" + magazinesize + "&elementalDamagePerSecond=" + elementalDamagePerSecond + "&chance=" + chance, function (data, status) {
                var json = $.parseJSON(data);
                $("#damageTimesFireRate").val(json["DamageTimesFireRate"]);
                $("#damageTimesFireRateTimesMagazineSize").val(json["DamageTimesFireRateTimesMagazineSize"]);
                $("#damageTimesFireRateTimesMagazineSizePerReloadSpeed").val(json["DamageTimesFireRateTimesMagazineSizePerReloadSpeed"]);
                $("#damageOnTarget").val(Number(json["DamageOnTarget"]).toLocaleString());
                $("#elementalDamagePerSecondTimesChance").val(json["ElementalDamagePerSecondTimesChance"]);
                $("#elementalDamageOnTargetTimesDamagePerSecondTimesChance").val(Number(json["ElementalDamageOnTargetTimesDamagePerSecondTimesChance"]).toLocaleString());
            });



        }



    });

    $("#damage, #accuracy,  #firerate, #reloadspeed, #magazinesize, #damageOnTarget").on("blur", function () {

        var element = $(this);
        var id = element.prop("id");
        var content = element.val();
        var type = $("#guntype").val();
        var rankall = element.parent().parent().find("[data-role=rankall]");
        var ranktype = element.parent().parent().find("[data-role=ranktype]");

        console.log([element, id, content, type, rankall, ranktype]);

        rankall.addClass("hidden");
        ranktype.addClass("hidden");

        if (type.length > 0 & content.length > 0) {
            $.get("/Guns/MetricRank?metric=" + id + "&value=" + content + "&type=" + type, function (data, status) {
                var json = $.parseJSON(data);
                rankall.text("[" + json[0]["allrank"] + "/" + json[0]["allrankcount"] + "]");
                ranktype.text("[" + json[0]["typerank"] + "/" + json[0]["typerankcount"] + "]");

                rankall.removeClass("hidden");
                ranktype.removeClass("hidden");

            });
        }


    });









    //create.end.





});