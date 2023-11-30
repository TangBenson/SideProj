import { React, useState, useEffect } from 'react';
import {
  GoogleMap,
  LoadScript,
  Marker,
  InfoWindow,
} from '@react-google-maps/api';
import carLocationIcon from './location.svg';

export default function Map() {
  const center = { lat: 23.9861321, lng: 121.5907741 };
  const carList = [
    { CarNo: "AAA-1234", CID: "", Latitude: 23.98703, Longitude: 121.58808 },
    { CarNo: "BBB-4321", CID: "", Latitude: 23.98338, Longitude: 121.58940 },
  ];
  const [hoveredCarIndex, setHoveredCarIndex] = useState(-1);
  const zoom = 15;

  const markerIcon = {
    url: carLocationIcon,
    scaledSize: { width: 75, height: 105 },
    labelOrigin: { x: 37, y: 85 },
  };

  return (
    <div className="main-container">
      <LoadScript
        googleMapsApiKey={"AIzaSyDtQ_pnDCr2GVlOEC1_7LWpKkdrh34FOkI"}
      >
        <GoogleMap
          mapContainerStyle={{ width: '100%', height: '900px' }}
          center={center}
          zoom={zoom}
          options={{
            streetViewControl: false,
            disableDefaultUI: true,
          }}
        >
          {carList.map((item, index) => (
            <Marker
              key={index}
              position={{
                lat: parseFloat(item.Latitude),
                lng: parseFloat(item.Longitude)
              }}
              title={item.CarNo}
              icon={markerIcon}
              label={item.CarNo}
            onMouseOver={() => {
              setHoveredCarIndex(index);
            }}
            onMouseOut={() => {
              setHoveredCarIndex(-1);
            }}
            >
              {hoveredCarIndex === index ? (
                <InfoWindow
                  // position={{
                  //   lat: parseFloat(item.Latitude),
                  //   lng: parseFloat(item.Longitude),
                  // }}
                >
                  <div>
                    {'VehicleMonitoringMap.searchBar.車號'}
                    : {item.CarNo}
                    <br />
                    CID: {item.CID}
                  </div>
                </InfoWindow>
              ) : null}
            </Marker>
          ))}
        </GoogleMap>
      </LoadScript>
    </div>
  );
}







// import GoogleMapReact from 'google-map-react';
// import React, { useEffect, useState } from 'react';

// const AnyReactComponent = ({ text }) => (
//   <div style={{
//     color: 'white',
//     background: 'grey',
//     padding: '15px 10px',
//     display: 'inline-flex',
//     textAlign: 'center',
//     alignItems: 'center',
//     justifyContent: 'center',
//     borderRadius: '100%',
//     transform: 'translate(-50%, -50%)'
//   }}>
//     <img src="/OIP.jpg" alt={text} />
//     {/* <div>{text}</div> */}
//   </div>
// );

// var a = 0;
// var b = 0;
// const Map = () => {

//   const defaultProps = {
//     center: {
//       lat: 23.9861321,
//       lng: 121.5907741
//     },
//     zoom: 15
//   };

//   const [markers, setMarkers] = useState([
//     { lat: 23.987030325794812, lng: 121.58808635961898, text: 5 },
//     { lat: 23.983388883570274, lng: 121.589409906775, text: 5 },
//     // 添加更多的標記...
//   ]);

//   const [mapState, setMapState] = useState(defaultProps);

//   const handleMapChange = (newMapState) => {
//     // console.log(newMapState);
//     // console.log(mapState);
//     setMapState({
//       center: newMapState.center,
//       zoom: newMapState.zoom
//     });
//   };

//   useEffect(() => {
//     console.log(a, b);
//     console.log("---------------");
//     console.log(markers[0].text, markers[1].text);
//     // 在地图状态变化时更新标记
//     setMarkers([
//       { lat: 23.987030325794812, lng: 121.58808635961898, text: a++ },
//       { lat: 23.983388883570274, lng: 121.589409906775, text: b++ }
//     ]);
//   }, [mapState]);

//   return (
//     // Important! Always set the container height explicitly

//     <div style={{ height: '100vh', width: '100%' }}>
//       <GoogleMapReact
//         bootstrapURLKeys={{ key: "AIzaSyDtQ_pnDCr2GVlOEC1_7LWpKkdrh34FOkI" }}
//         defaultCenter={defaultProps.center}
//         defaultZoom={defaultProps.zoom}
//         onChange={handleMapChange}
//         options={{
//           disableDefaultUI: true
//         }}
//       >
//         {markers.map((marker, index) => (
//           <AnyReactComponent
//             key={index}
//             lat={marker.lat}
//             lng={marker.lng}
//             text={marker.text}
//           />
//         ))}
//       </GoogleMapReact>
//     </div>
//   );
// }


// export default Map