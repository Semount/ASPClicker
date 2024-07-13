$(document).ready(function () {
    function updateUpgradeButtons() {
        var currentScore = parseFloat($("#scoreDisplay").text());
        $('.upgrade-btn').each(function () {
            var cost = parseFloat($(this).data('cost'));
            if (currentScore >= cost) {
                $(this).prop('disabled', false);
            } else {
                $(this).prop('disabled', true);
            }
        });
    }

    function updateModificationButtons() {
        var currentScore = parseFloat($("#scoreDisplay").text());
        $('.modification-btn').each(function () {
            var cost = parseFloat($(this).data('cost'));
            if (currentScore >= cost) {
                $(this).prop('disabled', false);
            } else {
                $(this).prop('disabled', true);
            }
        });
    }

    function applyDiscountToUpgrades(discount) {
        $('.upgrade-btn').each(function () {
            var originalCost = parseFloat($(this).data('original-cost'));
            var discountedCost = originalCost * (1 - discount / 100);
            $(this).data('cost', discountedCost);
            $(this).siblings('.upgrade-cost').find('.cost-value').text(discountedCost.toFixed(2));
        });
    }

    $('#clickCircle').click(function (event) {
        $.ajax({
            type: "POST",
            url: "/BaseGame/Click",
            success: function (data) {
                $("#scoreDisplay").text(data.score.toFixed(2));
                $("#pointsPerClick").text(data.clickPower.toFixed(2));
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
                console.error("Ошибка AJAX запроса: " + status + ", " + error);
            }
        });
    });

    $('.upgrade-btn').click(function () {
        var upgradeId = $(this).attr('id');
        var button = $(this);

        $.ajax({
            type: "POST",
            url: "/BaseGame/BuyUpgrade",
            data: { upgradeId: upgradeId },
            success: function (data) {
                $("#scoreDisplay").text(data.score.toFixed(2));

                // Обновление количества купленных улучшений и цены улучшения
                var countElement = $('#' + data.upgradeId + '_count');
                countElement.text(data.count);

                button.data('cost', data.cost);
                button.data('original-cost', data.cost / (1 - data.discount / 100));  // Обновляем оригинальную стоимость
                button.siblings('.upgrade-cost').find('.cost-value').text(data.cost.toFixed(2));
                $("#pointsPerClick").text(data.clickPower.toFixed(2));

                // Применение скидки к улучшениям
                applyDiscountToUpgrades(data.discount);

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Ошибка AJAX запроса: " + status + ", " + error);
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
                $("#scoreDisplay").text(data.score.toFixed(2));
                $("#pointsPerClick").text(data.clickPower.toFixed(2));
                $("#critChance").text(data.critChance.toFixed(2)); // Обновляем шанс критического клика

                // Применение скидки к улучшениям
                applyDiscountToUpgrades(data.discount);

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Ошибка AJAX запроса: " + status + ", " + error);
            }
        });
    });

    updateUpgradeButtons();
    updateModificationButtons();
});