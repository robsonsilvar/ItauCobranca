function GetParametro(param) {
	var urlParams = new URLSearchParams(window.location.search)
	return urlParams.get(param);
}