const stepIndex = +$('input[name=stepIndex]').val();
    function changeStepPageAndSubmit(index, newIndex) {
        $('[id^="stepIndex"]').each(function () {
            $(this).val(newIndex);
        });
        if (index == 0) {
            $('#updatePublicationForm').submit();
        }
        if (index == 1) {
            $('#updateThemeForm').submit();
        }
        if (index == 2) {
            $('#updateOtherForm').submit();
        }
        if (index == 3) {
            $('#updateFinishForm').submit();
        }
        if (index == 4) {
            $('#finalizeForm').submit();
        }
    };

    function handleNextStep() {
        debugger;
        changeStepPageAndSubmit(stepIndex, stepIndex + 1);
    };

    function handlePreviousStep() {
        changeStepPageAndSubmit(stepIndex, stepIndex - 1);
    };

    function onChangeStep(event, currentIndex, newIndex) {
        changeStepPageAndSubmit(currentIndex, newIndex);
        return true;
    };

    function updateSteps() {
        var settings = {
            headerTag: "h3",
            bodyTag: "section",
            stepsOrientation: "vertical",
            transitionEffect: "slideLeft",
            autoFocus: true,
            titleTemplate: "#title#",
            next: 'Далі',
            finish: 'Завершити',
            previous: 'Назад',
            startIndex: stepIndex,
            stepsOrientation: 'vertical',
            onStepChanging: function (event, currentIndex, newIndex) { return onChangeStep(event, currentIndex, newIndex); },
            transitionEffect: $.fn.steps.transitionEffect.none,
            transitionEffectSpeed: 0,
            enableAllSteps: true,
            onInit: function () {
                $("#wizard").css('display', 'block');
            },
            preloadContent: true,
            labels: {
                finish: "Завершити",
                next: "Далі",
                previous: "Назад"
            }
        };
        $('#wizard').steps(settings);
    };

    function events() {
        $('[id^="signButton"]').on('click', function (ev) {
            const confirm = confirm(`Після підписання ви не будете мати право редагувати звіт.
                                         Лише завідувач кафедри може зняти підпис.
                                         Ви впевнені, що бажаєте підписати звіт?`);
            return confirm;
        });
    }

    updateSteps();
    events();