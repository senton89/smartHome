import React, { useEffect, useState } from 'react';
import { getRooms, createRoom, updateRoom } from "../services/roomService";
import RoomForm from "./Rooms/RoomForm";
import './TopBar.css'; // Импортируем стили

const TopBar = ({ onSelectRoom }) => {
    const [rooms, setRooms] = useState([]);
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [selectedRoom, setSelectedRoom] = useState(null); // State for the selected room

    useEffect(() => {
        const fetchRooms = async () => {
            const data = await getRooms({});
            setRooms(data);
        };
        fetchRooms();
    }, []);

    const handleAddRoom = async (roomData) => {
        try {
            const createdRoom = await createRoom(roomData);
            setRooms((prevRooms) => [...prevRooms, createdRoom]); // Update the rooms list
            setIsFormVisible(false); // Hide the form after adding
        } catch (error) {
            console.error("Error adding room:", error);
        }
    };

    const handleUpdateRoom = async (roomData) => {
        try {
            await updateRoom(selectedRoom.id, roomData);
            setRooms((prevRooms) =>
                prevRooms.map((room) => (room.id === selectedRoom.id ? { ...room, ...roomData } : room))
            ); // Update the rooms list
            setIsFormVisible(false); // Hide the form after updating
            setSelectedRoom(null); // Clear the selected room
        } catch (error) {
            console.error("Error updating room:", error);
        }
    };

    const handleRoomClick = (room) => {
        setSelectedRoom(room);
        setIsFormVisible(true);
    };

    const handleRoomSelected = (room) => {
        onSelectRoom(room);
    };

    return (
        <div className="topbar-container">
            {rooms.map((room) => (
                <button
                    key={room.id}
                    onClick={() => handleRoomSelected(room)}
                    onDoubleClick={() => handleRoomClick(room)}
                    className="room-button"
                >
                    {room.name}
                </button>
            ))}
            <button
                onClick={() => {
                    setSelectedRoom(null); // Clear selected room for new room creation
                    setIsFormVisible(!isFormVisible);
                }}
                className="add-room-button"
            >
                +
            </button>
            {isFormVisible && (
                <RoomForm
                    room={selectedRoom} // Pass the selected room for editing
                    onSuccess={() => {
                        setIsFormVisible(false);
                        setSelectedRoom(null); // Clear selected room after success
                    }}
                />
            )}
        </div>
    );
};

export default TopBar;