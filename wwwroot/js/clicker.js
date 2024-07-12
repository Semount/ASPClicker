$(document).ready(function () {
    updateUpgradeButtons();
    updateModificationButtons();

    $('#clickCircle').click(function (event) {
        $.ajax({
            type: "POST",
            url: "/BaseGame/Click",
            success: function (data) {
                $("#scoreDisplay").text(data.score);
                $("#pointsPerClick").text(data.clickPower);
                updateUpgradeButtons();
                updateModificationButtons();

                // Добавление анимации появления очков
                var pointsPopup = $('<div class="points-popup">+' + data.clickPower + '</div>');
                pointsPopup.css({
                    top: event.pageY - 20,
                    left: event.pageX - 20
                });
                $('body').append(pointsPopup);
                pointsPopup.animate({ opacity: 0 }, 1000, function () {
                    pointsPopup.remove();
                });
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
                $("#scoreDisplay").text(data.score);
                $("#pointsPerClick").text(data.clickMultiplier);

                // Обновление количества купленных улучшений и цены улучшения
                var countElement = $('#' + data.upgradeId + '_count');
                countElement.text(data.count);

                button.data('cost', data.cost);
                button.siblings('.upgrade-cost').find('.cost-value').text(data.cost);

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
            url: "/BaseGame/PurchaseModification",
            data: { modificationId: modificationId },
            success: function (data) {
                $("#scoreDisplay").text(data.score);
                $("#" + modificationId + "_count").text(data.modificationCount);

                updateUpgradeButtons();
                updateModificationButtons();
            },
            error: function (xhr, status, error) {
                console.error("Ошибка AJAX запроса: " + status + ", " + error);
            }
        });
    });

    function updateUpgradeButtons() {
        var currentScore = parseInt($("#scoreDisplay").text());
        $('.upgrade-btn').each(function () {
            var cost = parseInt($(this).data('cost'));
            if (currentScore >= cost) {
                $(this).prop('disabled', false);
            } else {
                $(this).prop('disabled', true);
            }
        });
    }

    function updateModificationButtons() {
        var currentScore = parseInt($("#scoreDisplay").text());
        $('.modification-btn').each(function () {
            var cost = parseInt($(this).data('cost'));
            if (currentScore >= cost) {
                $(this).prop('disabled', false);
            } else {
                $(this).prop('disabled', true);
            }
        });
    }
});