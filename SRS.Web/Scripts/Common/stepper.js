const stepIndex = +$('input[name=stepIndex]').val();
function handleNextStep() {
    changeStepPageAndSubmit(stepIndex, stepIndex + 1);
};

function handlePreviousStep() {
    changeStepPageAndSubmit(stepIndex, stepIndex - 1);
};

function onChangeStep(event, currentIndex, newIndex) {
    return changeStepPageAndSubmit(currentIndex, newIndex);
};

function updateSteps() {
    var settings = {
        headerTag: "h3",
        bodyTag: "section",
        stepsOrientation: "vertical",
        transitionEffect: "slideLeft",
        autoFocus: true,
        titleTemplate: "#title#",
        next: 'Зберегти і перейти далі',
        finish: 'Завершити',
        previous: 'Назад',
        startIndex: stepIndex,
        stepsOrientation: 'vertical',
        onStepChanging: function (event, currentIndex, newIndex) {
            return onChangeStep(event, currentIndex, newIndex);
        },
        transitionEffect: $.fn.steps.transitionEffect.none,
        transitionEffectSpeed: 0,
        enableAllSteps: true,
        enableFinishButton: false,
        onInit: function () {
            $("#wizard").css('display', 'block');
        },
        preloadContent: true,
        labels: {
            finish: "Завершити",
            next: "Зберегти і перейти далі",
            previous: "Назад"
        }
    };
    $('#wizard').steps(settings);
};

updateSteps();