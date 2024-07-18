$(document).ready(function () {
    function updateUpgradeButtons() {
        var currentScoreYin = parseFloat($("#scoreYinDisplay").text());
        var currentScoreYang = parseFloat($("#scoreYangDisplay").text());

        $('.yin-upgrade-btn').each(function () {
            var cost = parseFloat($(this).data('cost'));
            $(this).prop('disabled', currentScoreYang < cost);
        });

        $('.yang-upgrade-btn').each(function () {
            var cost = parseFloat($(this).data('cost'));
            $(this).prop('disabled', currentScoreYin < cost);
        });
    }

    function updateModificationButtons() {
        var currentScoreYin = parseFloat($("#scoreYinDisplay").text());
        var currentScoreYang = parseFloat($("#scoreYangDisplay").text());

        $('.modification-btn').each(function () {
            var yinCost = parseFloat($(this).siblings('.modification-cost').find('.cost-value').first().text());
            var yangCost = parseFloat($(this).siblings('.modification-cost').find('.cost-value').last().text());
            $(this).prop('disabled', currentScoreYin < yinCost || currentScoreYang < yangCost);
        });
    }

    function calculateUpgradeCost(baseCost, count, discount) {
        var newCost = baseCost * Math.pow(1.15, count); // Базовая цена + 15% за каждое улучшение
        return newCost * (1 - discount / 100); // Применяем скидку
    }

    function applyDiscountToUpgrades(discountYin, discountYang) {
        $('.yin-upgrade-btn').each(function () {
            var baseCost = parseFloat($(this).data('base-cost'));
            var count = parseInt($(this).data('count'), 10);
            var discountedCost = calculateUpgradeCost(baseCost, count, discountYang);
            $(this).data('cost', discountedCost);
            $(this).siblings('.upgrade-cost').find('.cost-value').text(discountedCost.toFixed(2));
        });

        $('.yang-upgrade-btn').each(function () {
            var baseCost = parseFloat($(this).data('base-cost'));
            var count = parseInt($(this).data('count'), 10);
            var discountedCost = calculateUpgradeCost(baseCost, count, discountYin);
            $(this).data('cost', discountedCost);
            $(this).siblings('.upgrade-cost').find('.cost-value').text(discountedCost.toFixed(2));
        });
    }

    $('#clickYinCircle').click(function (event) {
        $.ajax({
            type: "POST",
            url: "/BaseGame/Click",
            data: { circleType: "Yin" },
            success: function (data) {
                $("#scoreYinDisplay").text(data.score.toFixed(2));
                $("#pointsPerClickYin").text(data.clickPower.toFixed(2));
                updateUpgradeButtons();
                updateModificationButtons();

                var pointsPopup = $('<div class="points-popup">+' + (data.isCritical ? (data.clickPower * 2).toFixed(2) : data.clickPower.toFixed(2)) + '</div>');
                pointsPopup.css({
                    top: event.pageY - 20,
                    left: event.pageX - 20
                });
                $('body').append(pointsPopup);
                pointsPopup.animate({ opacity: 0 }, 1000, function () {
                    pointsPopup.remove();
                });

                if (data.isCritical) {
                    $("#criticalClickMessage").show().delay(2000).fadeOut();
                    var criticalClickPopup = $('<div class="critical-click-popup">CRITICAL HIT!</div>');
                    criticalClickPopup.css({
                        top: event.pageY - 50,
                        left: event.pageX - 50
                    });
                    $('body').append(criticalClickPopup);
                    criticalClickPopup.animate({ opacity: 0, fontSize: '2em' }, 1000, function () {
                        criticalClickPopup.remove();
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error("Error in AJAX request: " + status + ", " + error);
            }
        });
    });

    $('#clickYangCircle').click(function (event) {
        $.ajax({
            type: "POST",
            url: "/BaseGame/Click",
            data: { circleType: "Yang" },
            success: function (data) {
                $("#scoreYangDisplay").text(data.score.toFixed(2));
                $("#pointsPerClickYang").text(data.clickPower.toFixed(2));
                updateUpgradeButtons();
                updateModificationButtons();

                var pointsPopup = $('<div class="points-popup">+' + (data.isCritical ? (data.clickPower * 2).toFixed(2) : data.clickPower.toFixed(2)) + '</div>');
                pointsPopup.css({
                    top: event.pageY - 20,
                    left: event.pageX - 20
                });
                $('body').append(pointsPopup);
                pointsPopup.animate({ opacity: 0 }, 1000, function () {
                    pointsPopup.remove();
                });

                if (data.isCritical) {
                    $("#criticalClickMessage").show().delay(2000).fadeOut();
                    var criticalClickPopup = $('<div class="critical-click-popup">CRITICAL HIT!</div>');
                    criticalClickPopup.css({
                        top: event.pageY - 50,
                        left: event.pageX - 50
                    });
                    $('body').append(criticalClickPopup);
                    criticalClickPopup.animate({ opacity: 0, fontSize: '2em' }, 1000, function () {
                        criticalClickPopup.remove();
                    });
                }
            },
            error: function (xhr, status, error) {
                console.error("Error in AJAX request: " + status + ", " + error);
            }
        });
    });

    $('.yin-upgrade-btn').click(function () {
        var upgradeId = $(this).attr('id');
        var button = $(this);

        $.ajax({
            type: "POST",
            url: "/BaseGame/BuyYinUpgrade",
            data: { upgradeId: upgradeId },
            success: function (data) {
                $("#scoreYinDisplay").text(data.scoreYin.toFixed(2));
                $("#scoreYangDisplay").text(data.scoreYang.toFixed(2));
                $("#pointsPerClickYin").text(data.clickPowerYin.toFixed(2));

                var countElement = $('#' + data.upgradeId + '_count');
                countElement.text(data.count);

                button.data('count', data.count);
                button.data('cost', data.cost);
                button.siblings('.upgrade-cost').find('.cost-value').text(data.cost.toFixed(2));

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Error in AJAX request: " + status + ", " + error);
            }
        });
    });

    $('.yang-upgrade-btn').click(function () {
        var upgradeId = $(this).attr('id');
        var button = $(this);

        $.ajax({
            type: "POST",
            url: "/BaseGame/BuyYangUpgrade",
            data: { upgradeId: upgradeId },
            success: function (data) {
                $("#scoreYinDisplay").text(data.scoreYin.toFixed(2));
                $("#scoreYangDisplay").text(data.scoreYang.toFixed(2));
                $("#pointsPerClickYang").text(data.clickPowerYang.toFixed(2));

                var countElement = $('#' + data.upgradeId + '_count');
                countElement.text(data.count);

                button.data('count', data.count);
                button.data('cost', data.cost);
                button.siblings('.upgrade-cost').find('.cost-value').text(data.cost.toFixed(2));

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Error in AJAX request: " + status + ", " + error);
            }
        });
    });

    $('.modification-btn').click(function () {
        var modificationId = $(this).attr('id');
        var button = $(this);

        $.ajax({
            type: "POST",
            url: "/BaseGame/BuyModification",
            data: { modificationId: modificationId },
            success: function (data) {
                $("#scoreYinDisplay").text(data.scoreYin.toFixed(2));
                $("#scoreYangDisplay").text(data.scoreYang.toFixed(2));
                $("#pointsPerClickYin").text(data.clickPowerYin.toFixed(2));
                $("#pointsPerClickYang").text(data.clickPowerYang.toFixed(2));
                $("#critChanceYin").text(data.critChanceYin);
                $("#critChanceYang").text(data.critChanceYang);
                applyDiscountToUpgrades(Number(data.discountYin), Number(data.discountYang));

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Error in AJAX request: " + status + ", " + error);
            }
        });
    });

    // Инициализация базовой стоимости апгрейдов
    $('.yin-upgrade-btn').each(function () {
        var baseCost = parseFloat($(this).siblings('.upgrade-cost').find('.cost-value').text());
        $(this).data('base-cost', baseCost);
        $(this).data('count', parseInt($(this).siblings('span').text(), 10) || 0);
    });

    $('.yang-upgrade-btn').each(function () {
        var baseCost = parseFloat($(this).siblings('.upgrade-cost').find('.cost-value').text());
        $(this).data('base-cost', baseCost);
        $(this).data('count', parseInt($(this).siblings('span').text(), 10) || 0);
    });

    updateUpgradeButtons();
    updateModificationButtons();
});