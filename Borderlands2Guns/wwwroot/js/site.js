// Write your JavaScript code.

$(document).ready(function () {


    //index.begin....

    $("#gunlist").DataTable( );

    //index.end.


    //index1.begin..
    /*
    var gunIndexTable = $("#guns-index-table").DataTable({
        "processing":true,
        "serverSide": true,
        "ajax": {
            "url": "/Guns/IndexData",
            "type":'GET'
        },
        "columns": [
            {"data":"name"}
        ],
        "columnsDefs": [
            {
                "targets": 0,
                "searchable": true
            }
        ]
    });

    console.log(gunIndexTable);
    */

    //index1.end.



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
        else
        {
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
                resultsDiv.append("<div class='row'> <div class='col-md-1'>" + json[i].Level + "</div> <div class='col-md-4'>" + json[i].Name + "</div> <div class='col-md-2'>" + json[i].DamageOnTarget + "</div> <div class='col-md-2'>" + json[i].AllTypesDamageOnTargetRank + "</div> </div>");
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
        var hostname = window.location.hostname;

        $.get("/Guns/GunCalcs?damage=" + damage + "&accuracy=" + accuracy + "&firerate=" + firerate + "&reloadspeed=" + reloadspeed + "&magazinesize=" + magazinesize + "&elementalDamagePerSecond=" + elementalDamagePerSecond + "&chance=" + chance, function (data, status) {

            var json = $.parseJSON(data);
            $("#damageTimesFireRate").val(json["DamageTimesFireRate"]);
            $("#damageTimesFireRateTimesMagazineSize").val(json["DamageTimesFireRateTimesMagazineSize"]);
            $("#damageTimesFireRateTimesMagazineSizePerReloadSpeed").val(json["DamageTimesFireRateTimesMagazineSizePerReloadSpeed"]);
            $("#damageOnTarget").val(json["DamageOnTarget"]);
            $("#elementalDamagePerSecondTimesChance").val(json["ElementalDamagePerSecondTimesChance"]);
            $("#elementalDamageOnTargetTimesDamagePerSecondTimesChance").val(json["ElementalDamageOnTargetTimesDamagePerSecondTimesChance"]);


        });

    });
    //create.end.






});