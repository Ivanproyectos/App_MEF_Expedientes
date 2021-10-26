/*
	Copyright (c) 2009 Hermann Schimpf (http://gschimpf.com/)
	Bajo licencia MIT y GPL.
	
	.: AllowChars v0.3 :.
	Probado en jQuery 1.3.2
	Actualizado a la Fecha: 2009-07-31
	
	Version 0.1: (2009-07-27)
	Modo de uso:
		simple:
			permite letras, numeros, y espacios
			$('selector').allowChars();
		personalizado:
			$('selector').allowChars({
				letras: boolean,
				numeros: boolean,
				espacios: boolean,
				caracteres: string,		// ejemplo: caracteres: '.-@/+'
				setfocus: boolean		// pone el foco sobre el objeto
			});
		otros:
			Si el formulario donde se utiliza esta ordenado con las propiedades 'tabindex' de cada elemento,
			se le puede pasar como parametro 'enter' en true o false para que al presionar la tecla enter
			automaticamente pase al siguiente campo.
			Asi como tambien la cantidad de campos que existen. Por defecto es 20.
			Este parametro hace que al llegar al ultimo campo y presionar la tecla enter,
			se envie el foco al primer campo.
			No hay problema en poner una cantidad mayor a la que existiere en el formulario.
			La funcion detectara que ya no existen mas campos y volvera al inicial
			
			$('selector').allowChars({
				enter: true,
				maxFields: 15
			});
			
	Version 0.2: (2009-07-30)
		agregados los eventos 'onfocus' y 'onblur'
		
		$('selector').allowChars({
			onfocus: function(event) {
				... aqui el desarrollo del codigo ...
			},
			onblur: function() {
				... aqui el desarrollo del codigo ...
			};
		});
		
	Version 0.3: (2009-08-03)
		Solucionado el problema con la tecla Shift cuando se pertimen todos los caracteres
		Agregado los eventos onpaste, oncopy, oncut
		
		$('selector').allowChars({
			onpaste: function(event) {
				... aqui el desarrollo del codigo ...
			},
			oncopy: function(event) {
				... aqui el desarrollo del codigo ...
			},
			oncut: function(event) {
				... aqui el desarrollo del codigo ...
			};
		});
		
		Para deshabilitar los eventos de copiar, pegar y cortar se puede enviar como parametro 'False'
		el cual se desea deshabilitar:
		
		$('selector').allowChars({
			onpaste: false,
			oncopy: false,
			oncut: false
		});
*/
	
(function($) {
	$.fn.allowChars = function(options) {
		/* tomamos las configuraciones por defecto */
		options = $.extend({}, $.fn.allowChars.defaults, options);
		/* obtenemos el tabindex del campo actual */
		options.tabindex = $(this).attr('tabindex');
		/* creamos un atributo que contendra el objeto actual */
		options.selected = $(this);
		/*  asignamos los eventos */
		$(this)
			/* en el evento keydown seteamos la funcion general */
			.bind('keydown',function(event) {
				/* asignamos el evento a una propuedad para poder manejarlo */
				options.event = event;
				/* retornamos la funcion general */
				return $.fn.allowChars.eventInput(options);
			})
			/* asignamos los eventos onBlur y onFocus */
			.bind('blur', options.onblur)
			.bind('focus', options.onfocus);
			
		/* si se recibio como parametros que se setee el foco el el objeto le asignamos focus() */
		if (options.setfocus === true)
			$(this).focus();
			
		/* si por parametros se recibio que se deshabilite el evento oncopy asignamos una funcion que retorne false */
		if (options.oncopy === false)
			$(this).bind('copy', function() {
				return false;
			});
		/* sino, si se recibio una funcion, asignamos la misma al evento copy */
		else if (typeof options.oncopy == 'function')
			$(this).bind('copy', options.oncopy);
			
		/* si por parametros se recibio que se deshabilite el evento onpaste asignamos una funcion que retorne false */
		if (options.onpaste === false)
			$(this).bind('paste', function() {
				return false;
			});
		/* sino, si se recibio una funcion, asignamos la misma al evento paste */
		else if (typeof options.onpaste == 'function')
			$(this).bind('paste', options.onpaste);
			
		/* si por parametros se recibio que se deshabilite el evento oncut asignamos una funcion que retorne false */
		if (options.oncut === false)
			$(this).bind('cut', function() {
				return false;
			});
		/* sino, si se recibio una funcion, asignamos la misma al evento cut */
		else if (typeof options.oncut == 'function')
			$(this).bind('cut', options.oncut);
	}
	$.fn.allowChars.defaults = {
		/* valores por defecto: permite letras, numeros y espacios */
		letras: true,
		numeros: true,
		espacios: true,
		caracteres: false,
		
		/* Al presionar 'ctrl + enter' pasar al ultimo tabindex */
		ctrlenter: false,
		
		/*	El event toma valor en el momento de la llamada de la funcion.	*/
		/*	No sirve enviar como parametro otra cosa porque sera remplazada	*/
		setfocus: false,
		enter: false,
		maxfields: 20,
		
		/* El name toma el nombre del campo. */
		name: $(this).attr('name'),
		
		/* Eventos de salida y foco */
		onblur: function(e){},
		onfocus: function(e){},
		
		/* Eventos de copiar, pegar y cortar */
		oncopy: function(e){},
		onpaste: function(e){},
		oncut: function(e){}
	}
	$.fn.allowChars.charOfKeyCode = function(caracter) {
		/* creamos un array con los simbolos y sus keyCode */
		var keyCodes = new Array('! 49','" 222','$ 52','% 53','& 55','/ 191','( 57',') 48','= 61','? 191','* 56','; 59',': 59','> 190','\\ 220','| 220','@ 50','# 51','~ 192','{ 219','[ 219','] 221','} 221','< 188','/ 111','- 109','+ 107','. 110',', 188','_ 109','. 190');
		var item;
		var retorno = '';
		/* recorremos los simbolos */
		for (var i in keyCodes) {
			/* separamos el caracter del codigo, ej: '! 49' > ['!', 49] */
			item = keyCodes[i].split(' ');
			if (item[0] == caracter)
				/*
					si el caracter es igual al recibido por parametros agregamos el codigo a la variable de retorno
					puede que un keycode posea varios caracteres
				*/
				retorno += item[1] + ' ';
		}
		/* eliminamos el ultimo espacio de la variable de retorno, ej: '221 221 ' > '221 221' */
		retorno = retorno.substring(0,(retorno.length - 1));
		/* retornamos la cadena con los keycodes */
		return retorno;
	}
	$.fn.allowChars.eventInput = function(params) {
		/* obtenemos el keycode desde el evento which */
		var key = params.event.which;
		/* creamos un array donde guardaremos todos los keycode permitidos */
		var arrayKeys = new Array();
		
		/* Agregamos los keycode correspondientes a las funciones de teclado comunes */
		/* (33-36) RePag, AvPag, Fin, Inicio */
		/* (37-40) Flechas de navegacion	 */
		for (var i = 33; i <= 40; i++)
			arrayKeys[i] = i;
		arrayKeys[8] = 8;	/* backspace */
		arrayKeys[9] = 9;	/* tabulador */
		arrayKeys[27] = 27;	/* escape */
		arrayKeys[46] = 46;	/* suprimir	 */
		arrayKeys[190] = 190;	/* punto	 */
		arrayKeys[110] = 110;	/* punto	 */
		/* teclas F1, F2, F3.. */
		for (var i = 112; i <= 123; i++)
			arrayKeys[i] = i;
		
		/* Verificamos si se permiten letras */
		if (params.letras === true)
			/* si se permiten letras, agregamos los keycode */
			for (var i = 65; i <= 90; i++)
				arrayKeys[i] = i;
		/* Verificamos si se permiten numeros */
		if (params.numeros === true) {
			/* si se permiten numeros, agregamos los keycode */
			for (var i = 48; i <= 57; i++)
				arrayKeys[i] = i;	/* Numeros encima del teclado */
			for (var i = 96; i <= 105; i++)
				arrayKeys[i] = i;	/* Teclado numerico */
		}
		/* verificamos si se permiten espacios */
		if (params.espacios === true)
			arrayKeys[32] = 32;
		/* Verificamos si se permiten otros caracteres */
		if (params.caracteres === true) {
			/* permite ingresar todo menos espacios */
			for (var i = 0; i <= 31; i++)
				arrayKeys[i] = i;
			for (var i = 33; i <= 255; i++)
				arrayKeys[i] = i;
		} else if (params.caracteres !== false) {
			/* si se permiten otros caracteres descomponemos la cadena y agregamos los keycode */
			var indice;
			var permitidos = new Array;
			//alert(params.caracteres);
			permitidos = params.caracteres.split('');
			for (var l in permitidos) {
				/* recorro el array y agrelo los keycode de cada caracter */
				/* obtengo el keycode desde la funcion charOfKeyCode() */
				indice = $.fn.allowChars.charOfKeyCode(permitidos[l]);
				/* si lo que recibo no es un numero es porque obtuve mas de un keycode, ej un string: '221 45' */
				if (isNaN(indice)) {
					/* armo un array de los keycodes separados por un espacio */
					indice = indice.split(' ');
					/* recorro el array agregando cada uno de los keycode a la matriz */
					for (var t in indice)
						arrayKeys[indice[t]] = indice[t];
				} else
					/* si lo que devolvio la funcion es un numero unico de keycode lo agrego a la matriz */
					arrayKeys[indice] = indice;
			}
		}
		if (!params.event.shiftKey && !params.event.ctrlKey) {
		    
			/* recorremos el array para preguntar por el key presionado */
		    for (var i in arrayKeys) {
		         
				if (arrayKeys[i] == key) {
					/* si el key presionado esta dentro del array retornamos True */
					return true;
				} else if (key == 13 && params.enter === true) {
					/*
						si se preciona enter y esta habilitado el parametro
						para saltar al siguiente campo ejecuto este codigo
					*/
					if (typeof params.name == 'undefined') {
						/* verifico si se obtuvo el nombre del campo */
						alert('Falta el parametro con el nombre del campo');
						return false;
					}
					if (typeof params.tabindex == 'undefined') {
						/* verifico que el campo tenga la etiqueta 'tabindex' */
						alert('El campo no tiene la etiqueta \'tabindex\'');
						return false;
					}
					if (params.maxFields >= 100) {
						/* verifico la camtidad maxima de campos que se asigno a los parametros */
						alert('La cantidad maxima de campos es mayor a 100\nSe limito esta cantidad puesto que puede generar el bloqueo del navegador');
						return false;
					}
					/* obtengo el tabindex del campo actual y agrego 1 para pasar al siguiente */
					var tab = params.tabindex;
					tab++;
					/*
						puede darse el caso de que los tabindex no esten exactamente consecutivos, ej: 1, 2, 3, 5, 6..
						verifico que el siguiente tabindex exista y salto de a uno hasta encontrar un existente
					*/
					while (typeof $("[tabindex='" + tab + "']").get(0) == 'undefined') {
						tab++;
						/* si llegue al maximo de campos y no encontre un tabindex siguiente salto al campo inicial */
						if (tab >= params.maxfields) {
							tab = 1;
							break;
						}
					}
					/* llamo a la funcion onblur del campo actual */
					params.onblur();
					/* seteo el foco en el siguiente campo */
					$("[tabindex='" + tab + "']").focus();
					return false;
				}
			}
		} else {
			/* verifico si presiona la tecla shift */
			if (params.event.shiftKey) {
				/* permito las flechas de navegacion, inicio, fin, avpag, repag y tabulador */
				for (var i = 33; i <= 40; i++)
					if (i == key || key == 9)
						return true;
				/* si se permiten letras, acepto las mayusculas */
				if (params.letras)
					for (var i = 65; i <= 90; i++)
						if (i == key)
							return true;
				if (params.caracteres === true) {
					if (key != 32)
						return true;
				} else if (params.caracteres !== false) {
					/* si se permiten otros caracteres descomponemos la cadena y agregamos los keycode */
					var indice;
					var permitidos = new Array;
					//alert(params.caracteres);
					permitidos = params.caracteres.split('');
					for (var l in permitidos) {
						/* recorro el array y agrelo los keycode de cada caracter */
						indice = $.fn.allowChars.charOfKeyCode(permitidos[l]);
						/* si no es un nro es porque recibi mas de un codigo, ej: '110 55' */
						if (isNaN(indice)) {
							/* separo los codigos con un espacio */
							indice = indice.split(' ');
							/* recorro los codigos y pregunto si es igual al codigo presionado */
							for (var t in indice)
								if (indice[t] == key)
									/* retorno true si es correcto */
									return true;
						} else
							/* si es un nro pregunto directamente por el codigo precionado */
							if (indice == key)
								return true;
					}
				}
			} else if (params.event.ctrlKey) {
				/*
					Con la tecla control permito:
						Z, X, C y V para deshacer, cortar, copiar y pegar
						A, E para seleccionar todo (espanol e ingles)
				*/
				if (key == 65 || key == 67 || key == 69 || key == 86 || key == 88 || key == 90)
					return true;
				else if (key == 13 && params.ctrlenter === true) {
					/* si se preciona ctrl + enter y esta habilitado el parametro ctrlenter salto al ultimo tabindex */
					/* me posiciono en cantidad maxima de campos */
					var tab = params.maxfields;
					/* realizo un bucle mientras que no exista un objeto con el tabindex actual */
					while (typeof $("[tabindex='" + tab + "']").get(0) == 'undefined') {
						/* dismuniyo al tabindex anterior */
						--tab;
						if (tab <= 1 || typeof $("[tabindex='" + tab + "']").get(0) != 'undefined')
							/* si llegue a 1 o encontre el tabindex mayor salgo */
							break;
					}
					params.onblur();
					/* seteo el foco en el tabindex mayor */
					$("[tabindex='" + tab + "']").focus();
					return false;
				}
			}
		}
		/* si llego aqui es porque el keycode no esta permitido, retorno false */
		return false;
	}
})(jQuery);


(function ($) {
    $.fn.allowChars2 = function (options) {
        /* tomamos las configuraciones por defecto */
        options = $.extend({}, $.fn.allowChars2.defaults, options);
        /* obtenemos el tabindex del campo actual */
        options.tabindex = $(this).attr('tabindex');
        /* creamos un atributo que contendra el objeto actual */
        options.selected = $(this);
        /*  asignamos los eventos */
        $(this)
			/* en el evento keydown seteamos la funcion general */
			.bind('keydown', function (event) {
			    /* asignamos el evento a una propuedad para poder manejarlo */
			    options.event = event;
			    /* retornamos la funcion general */
			    return $.fn.allowChars2.eventInput(options);
			})
			/* asignamos los eventos onBlur y onFocus */
			.bind('blur', options.onblur)
			.bind('focus', options.onfocus);

        /* si se recibio como parametros que se setee el foco el el objeto le asignamos focus() */
        if (options.setfocus === true)
            $(this).focus();

        /* si por parametros se recibio que se deshabilite el evento oncopy asignamos una funcion que retorne false */
        if (options.oncopy === false)
            $(this).bind('copy', function () {
                return false;
            });
            /* sino, si se recibio una funcion, asignamos la misma al evento copy */
        else if (typeof options.oncopy == 'function')
            $(this).bind('copy', options.oncopy);

        /* si por parametros se recibio que se deshabilite el evento onpaste asignamos una funcion que retorne false */
        if (options.onpaste === false)
            $(this).bind('paste', function () {
                return false;
            });
            /* sino, si se recibio una funcion, asignamos la misma al evento paste */
        else if (typeof options.onpaste == 'function')
            $(this).bind('paste', options.onpaste);

        /* si por parametros se recibio que se deshabilite el evento oncut asignamos una funcion que retorne false */
        if (options.oncut === false)
            $(this).bind('cut', function () {
                return false;
            });
            /* sino, si se recibio una funcion, asignamos la misma al evento cut */
        else if (typeof options.oncut == 'function')
            $(this).bind('cut', options.oncut);
    }
    $.fn.allowChars2.defaults = {
        /* valores por defecto: permite letras, numeros y espacios */
        letras: true,
        numeros: true,
        espacios: true,
        caracteres: false,

        /* Al presionar 'ctrl + enter' pasar al ultimo tabindex */
        ctrlenter: false,

        /*	El event toma valor en el momento de la llamada de la funcion.	*/
        /*	No sirve enviar como parametro otra cosa porque sera remplazada	*/
        setfocus: false,
        enter: false,
        maxfields: 20,

        /* El name toma el nombre del campo. */
        name: $(this).attr('name'),

        /* Eventos de salida y foco */
        onblur: function (e) { },
        onfocus: function (e) { },

        /* Eventos de copiar, pegar y cortar */
        oncopy: function (e) { },
        onpaste: function (e) { },
        oncut: function (e) { }
    }
    $.fn.allowChars2.charOfKeyCode = function (caracter) {
        /* creamos un array con los simbolos y sus keyCode */
        var keyCodes = new Array('! 49', '" 222', '$ 52', '% 53', '& 55', '/ 191', '( 57', ') 48', '= 61', '? 191', '* 56', '; 59', ': 59', '> 190', '\\ 220', '| 220', '@ 50', '# 51', '~ 192', '{ 219', '[ 219', '] 221', '} 221', '< 188', '/ 111', '- 109', '+ 107', '. 110', ', 188', '_ 109', '. 190');
        var item;
        var retorno = '';
        /* recorremos los simbolos */
        for (var i in keyCodes) {
            /* separamos el caracter del codigo, ej: '! 49' > ['!', 49] */
            item = keyCodes[i].split(' ');
            if (item[0] == caracter)
                /*
					si el caracter es igual al recibido por parametros agregamos el codigo a la variable de retorno
					puede que un keycode posea varios caracteres
				*/
                retorno += item[1] + ' ';
        }
        /* eliminamos el ultimo espacio de la variable de retorno, ej: '221 221 ' > '221 221' */
        retorno = retorno.substring(0, (retorno.length - 1));
        /* retornamos la cadena con los keycodes */
        return retorno;
    }
    $.fn.allowChars2.eventInput = function (params) {
        /* obtenemos el keycode desde el evento which */
        var key = params.event.which;
        /* creamos un array donde guardaremos todos los keycode permitidos */
        var arrayKeys = new Array();
        console.log(key);
        /* Agregamos los keycode correspondientes a las funciones de teclado comunes */
        /* (33-36) RePag, AvPag, Fin, Inicio */
        /* (37-40) Flechas de navegacion	 */
        for (var i = 33; i <= 40; i++)
            arrayKeys[i] = i;
        arrayKeys[8] = 8;	/* backspace */
        arrayKeys[9] = 9;	/* tabulador */
        arrayKeys[13] = 13;	/* enter	 */
        arrayKeys[27] = 27;	/* escape */
        arrayKeys[46] = 46;	/* suprimir	 */
         
        /* teclas F1, F2, F3.. */
        for (var i = 112; i <= 123; i++)
            arrayKeys[i] = i;

        /* Verificamos si se permiten letras */
        if (params.letras === true)
            /* si se permiten letras, agregamos los keycode */
            for (var i = 65; i <= 90; i++)
                arrayKeys[i] = i;
        /* Verificamos si se permiten numeros */
        if (params.numeros === true) {
            /* si se permiten numeros, agregamos los keycode */
            for (var i = 48; i <= 57; i++)
                arrayKeys[i] = i;	/* Numeros encima del teclado */
            for (var i = 96; i <= 105; i++)
                arrayKeys[i] = i;	/* Teclado numerico */
        }
        /* verificamos si se permiten espacios */
        //if (params.espacios === true)
        //    arrayKeys[32] = 32;
        /* Verificamos si se permiten otros caracteres */
        if (params.caracteres === true) {
            /* permite ingresar todo menos espacios */
            //for (var i = 0; i <= 31; i++)
            //    arrayKeys[i] = i;
            //for (var i = 33; i <= 255; i++)
            //    arrayKeys[i] = i;
        } else if (params.caracteres !== false) {
             
            /* si se permiten otros caracteres descomponemos la cadena y agregamos los keycode */
            var indice;
            var permitidos = new Array;
            //alert(params.caracteres);
            permitidos = params.caracteres.split('');
            for (var l in permitidos) {
                /* recorro el array y agrelo los keycode de cada caracter */
                /* obtengo el keycode desde la funcion charOfKeyCode() */
                indice = $.fn.allowChars2.charOfKeyCode(permitidos[l]);
                /* si lo que recibo no es un numero es porque obtuve mas de un keycode, ej un string: '221 45' */
                if (isNaN(indice)) {
                    /* armo un array de los keycodes separados por un espacio */
                    indice = indice.split(' ');
                    /* recorro el array agregando cada uno de los keycode a la matriz */
                    for (var t in indice)
                        arrayKeys[indice[t]] = indice[t];
                } else
                    /* si lo que devolvio la funcion es un numero unico de keycode lo agrego a la matriz */
                    arrayKeys[indice] = indice;
            }
        }
        if (params.event.altKey) return false;
        if (!params.event.shiftKey && !params.event.ctrlKey ) {
             
            /* recorremos el array para preguntar por el key presionado */
            for (var i in arrayKeys) {

                if (arrayKeys[i] == key) {
                    /* si el key presionado esta dentro del array retornamos True */
                    return true;
                } else if (key == 13 && params.enter === true) {
                    /*
						si se preciona enter y esta habilitado el parametro
						para saltar al siguiente campo ejecuto este codigo
					*/
                    if (typeof params.name == 'undefined') {
                        /* verifico si se obtuvo el nombre del campo */
                        alert('Falta el parametro con el nombre del campo');
                        return false;
                    }
                    if (typeof params.tabindex == 'undefined') {
                        /* verifico que el campo tenga la etiqueta 'tabindex' */
                        alert('El campo no tiene la etiqueta \'tabindex\'');
                        return false;
                    }
                    if (params.maxFields >= 100) {
                        /* verifico la camtidad maxima de campos que se asigno a los parametros */
                        alert('La cantidad maxima de campos es mayor a 100\nSe limito esta cantidad puesto que puede generar el bloqueo del navegador');
                        return false;
                    }
                    /* obtengo el tabindex del campo actual y agrego 1 para pasar al siguiente */
                    var tab = params.tabindex;
                    tab++;
                    /*
						puede darse el caso de que los tabindex no esten exactamente consecutivos, ej: 1, 2, 3, 5, 6..
						verifico que el siguiente tabindex exista y salto de a uno hasta encontrar un existente
					*/
                    while (typeof $("[tabindex='" + tab + "']").get(0) == 'undefined') {
                        tab++;
                        /* si llegue al maximo de campos y no encontre un tabindex siguiente salto al campo inicial */
                        if (tab >= params.maxfields) {
                            tab = 1;
                            break;
                        }
                    }
                    /* llamo a la funcion onblur del campo actual */
                    params.onblur();
                    /* seteo el foco en el siguiente campo */
                    $("[tabindex='" + tab + "']").focus();
                    return false;
                }
            }
        } else {
             
            /* verifico si presiona la tecla shift */
            if (params.event.shiftKey == 'xD') {
                /* permito las flechas de navegacion, inicio, fin, avpag, repag y tabulador */
                for (var i = 33; i <= 40; i++)
                    if (i == key || key == 9)
                        return true;
                /* si se permiten letras, acepto las mayusculas */
                if (params.letras)
                    for (var i = 65; i <= 90; i++)
                        if (i == key)
                            return true;
                if (params.caracteres === true) {
                    if (key != 32)
                        return true;
                } else if (params.caracteres !== false) {
                    /* si se permiten otros caracteres descomponemos la cadena y agregamos los keycode */
                    var indice;
                    var permitidos = new Array;
                    //alert(params.caracteres);
                    permitidos = params.caracteres.split('');
                    for (var l in permitidos) {
                        /* recorro el array y agrelo los keycode de cada caracter */
                        indice = $.fn.allowChars2.charOfKeyCode(permitidos[l]);
                        /* si no es un nro es porque recibi mas de un codigo, ej: '110 55' */
                        if (isNaN(indice)) {
                            /* separo los codigos con un espacio */
                            indice = indice.split(' ');
                            /* recorro los codigos y pregunto si es igual al codigo presionado */
                            for (var t in indice)
                                if (indice[t] == key)
                                    /* retorno true si es correcto */
                                    return true;
                        } else
                            /* si es un nro pregunto directamente por el codigo precionado */
                            if (indice == key)
                                return true;
                    }
                }
            } else if (params.event.ctrlKey == 'xD') {
                /*
					Con la tecla control permito:
						Z, X, C y V para deshacer, cortar, copiar y pegar
						A, E para seleccionar todo (espanol e ingles)
				*/
                if (key == 65 || key == 67 || key == 69 || key == 86 || key == 88 || key == 90)
                    return true;
                else if (key == 13 && params.ctrlenter === true) {
                    /* si se preciona ctrl + enter y esta habilitado el parametro ctrlenter salto al ultimo tabindex */
                    /* me posiciono en cantidad maxima de campos */
                    var tab = params.maxfields;
                    /* realizo un bucle mientras que no exista un objeto con el tabindex actual */
                    while (typeof $("[tabindex='" + tab + "']").get(0) == 'undefined') {
                        /* dismuniyo al tabindex anterior */
                        --tab;
                        if (tab <= 1 || typeof $("[tabindex='" + tab + "']").get(0) != 'undefined')
                            /* si llegue a 1 o encontre el tabindex mayor salgo */
                            break;
                    }
                    params.onblur();
                    /* seteo el foco en el tabindex mayor */
                    $("[tabindex='" + tab + "']").focus();
                    return false;
                }
            }
        }
        /* si llego aqui es porque el keycode no esta permitido, retorno false */
        return false;
    }
})(jQuery);