const stepIndex = +$('input[name=stepIndex]').val();
function handleNextStep() {
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
        enableFinishButton: false,
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

updateSteps();