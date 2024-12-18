import React, { useState } from 'react';
import '../src/styles/App.css'; // Импортируем стили
import TopBar from './components/TopBar';
import RoomDetails from '../src/components/Rooms/RoomDetails';
import YandexIntegration from '../src/components/Yandex/YandexIntegration';

const App = () => {
    const [selectedRoom, setSelectedRoom] = useState(null);

    const handleEditDevice = (device) => {
        // Логика редактирования устройства
    };

    const handleDeleteDevice = (deviceId) => {
        // Логика удаления устройства
    };

    return (
        <div className="app-container">
            {/*<h1 className="app-title">Smart Home Management</h1>*/}
            <TopBar onSelectRoom={setSelectedRoom} />
            <div className="main-content">
                <div className="room-details">
                    {selectedRoom && (
                        <RoomDetails
                            room={selectedRoom}
                            onEditDevice={handleEditDevice}
                            onDeleteDevice={handleDeleteDevice}
                        />
                    )}
                </div>
                <div className="yandex-integration">
                    <YandexIntegration />
                </div>
            </div>
        </div>
    );
};

export default App;