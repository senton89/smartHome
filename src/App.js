import React, { useState, useEffect } from 'react';
import '../src/styles/App.css'; // Импортируем стили
import TopBar from './components/TopBar';
import RoomDetails from '../src/components/Rooms/RoomDetails';
import YandexIntegration from '../src/components/Yandex/YandexIntegration';
import { deleteDevice, updateDevice, getDevicesByRoom } from "./services/deviceService"; // Import getDevices

const App = () => {
    const [selectedRoom, setSelectedRoom] = useState(null);
    const [devices, setDevices] = useState([]); // State to hold devices for the selected room

    // Fetch devices when the selected room changes
    useEffect(() => {
        const fetchDevices = async () => {
            if (selectedRoom) {
                try {
                    const roomDevices = await getDevicesByRoom(selectedRoom.id);
                    setDevices(roomDevices);
                } catch (error) {
                    console.error("Error fetching devices:", error);
                }
            }
        };

        fetchDevices();
    }, [selectedRoom]); // Dependency array includes selectedRoom

    const handleEditDevice = async (device) => {
        const updatedDeviceData = { ...device, name: 'Updated Device Name' }; // Example of updating the device name
        try {
            const updatedDevice = await updateDevice(device.id, updatedDeviceData);
            console.log('Device updated:', updatedDevice);
            // Optionally, update the devices state to reflect the changes
            setDevices((prevDevices) =>
                prevDevices.map((d) => (d.id === device.id ? updatedDevice : d))
            );
        } catch (error) {
            console.error("Error updating device:", error);
        }
    };

    const handleDeleteDevice = async (deviceId) => {
        try {
            await deleteDevice(deviceId);
            console.log('Device deleted:', deviceId);
            // Update the devices state to remove the deleted device
            setDevices((prevDevices) => prevDevices.filter((d) => d.id !== deviceId));
        } catch (error) {
            console.error("Error deleting device:", error);
        }
    };

    return (
        <div className="app-container">
            <TopBar onSelectRoom={setSelectedRoom} />
            <div className="main-content">
                <div className="room-details">
                    {selectedRoom && (
                        <RoomDetails
                            room={selectedRoom}
                            devices={devices} // Pass the devices to RoomDetails
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