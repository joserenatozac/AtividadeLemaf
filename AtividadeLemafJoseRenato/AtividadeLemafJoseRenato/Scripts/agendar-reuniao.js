$(function () {
    $(document).on('keydown', '#quantidade-pessoas', function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl/cmd+A
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+C
            (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: Ctrl/cmd+X
            (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }


    });
    $(document).on('click', '#btn-agendar', function () {
        if(validarParametros()){
            aoClicarAgendar();
        }
    });
});

function validarParametros() {
    if (!$('#data-inicio').val()) {
        alert('É necessário preencher a data de início');
        return false;
    }
    if (!$('#data-fim').val()) {
        alert('É necessário preencher a data de término');
        return false;
    }
    if (!$('#quantidade-pessoas').val()) {
        alert('É necessário preencher a quantidade de pessoas');
        return false;
    }
    return true;
}

function aoClicarAgendar() {

    $.ajax({
        type: "GET",
        url: urlAgendarReuniao,
        data: {
            'dataInicio' : '2018-01-01T01:00:00',
            'dataFim' : '2018-01-01T01:00:00',
            'quantidadePessoas' : 10,
            'precisaInternet' : true,
            'precisaWebcam' : false
        },
        dataType: 'json',
        traditional: true,
        async: true,
        complete: function () {
        },
        success: function (retorno) {
            if (retorno.sucesso) {
                alert('Agendamento feito com sucesso!');
            }
            else {
                alert(retorno.mensagemErro);
            }
        },
        error: function (retorno) {
            alert(retorno.mensagemErro);
        }
    });
}