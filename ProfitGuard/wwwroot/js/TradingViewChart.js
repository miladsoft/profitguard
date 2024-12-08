window.loadTradingViewWidget = function () {
    new TradingView.widget({
        container_id: "tradingview_chart",
        autosize: true,
        symbol: "COINEX:BTCUSDT",
        interval: "1",
        theme: "light",
        style: "1",
        locale: "en",
        enable_publishing: false,
        hide_top_toolbar: true,
        save_image: false,
        studies: [],
        show_popup_button: true,
        popup_width: "1000",
        popup_height: "650"
    });
};
