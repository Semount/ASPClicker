$(document).ready(function () {
    $('#clickCircle').click(function (event) {
        $.ajax({
            type: "POST",
            url: "/BaseGame/Click",
            success: function (data) {
                $("#scoreDisplay").text(data.score);
                $("#pointsPerClick").text(data.clickMultiplier);
                updateUpgradeButtons();

                // Добавление анимации появления очков
                var pointsPopup = $('<div class="points-popup">+' + data.clickMultiplier + '</div>');
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

});