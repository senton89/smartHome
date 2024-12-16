const express = require('express');
const app = express();
const yandexRoutes = require('./routes/yandex');

app.use(express.json());
app.use('/api/yandex', yandexRoutes);

const PORT = process.env.PORT || 5001;
app.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});

module.exports = app;