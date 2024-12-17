import React, { useState } from 'react';
import TopBar from './components/TopBar';
import RoomDetails from '../src/components/Rooms/RoomDetails';
import YandexIntegration from '../src/components/Yandex/YandexIntegration';

const App = () => {
    const [selectedRoom, setSelectedRoom] = useState(null);


    const handleEditDevice = (device) => {

    };

    const handleDeleteDevice = (deviceId) => {
        setDevices(devices.filter(device => device.id !== deviceId));
    };

    return (
        <div className="bg-purple-100 min-h-screen p-4">
            <h1 className="text-3xl font-bold text-center text-purple-800 mb-6">Smart Home Management</h1>
            <TopBar onSelectRoom={setSelectedRoom} />
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
                {selectedRoom && (
                    <RoomDetails
                        room={selectedRoom}
                        onEditDevice={handleEditDevice}
                        onDeleteDevice={handleDeleteDevice}
                    />
                )}
                <YandexIntegration />
            </div>
        </div>
    );
};

export default App;