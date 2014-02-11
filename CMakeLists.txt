cmake_minimum_required( VERSION 2.6 )
project( wc_sharp )

set( BUILD_WCSHARP_EXAMPLES FALSE CACHE BOOL "Build C# examples" )
if( BUILD_WCSHARP_EXAMPLES )
	add_subdirectory( examples )
else()
	message( STATUS "NOT building C# examples" )
endif()

find_package( Doxygen )
if( DOXYGEN_FOUND )
	if( NOT TARGET doc )
		add_custom_target( doc )
	endif()
	add_custom_target( doc_wc_sharp ${DOXYGEN_EXECUTABLE} Doxyfile
		WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}"
	)
	add_dependencies( doc doc_wc_sharp )
	set_directory_properties( PROPERTIES ADDITIONAL_MAKE_CLEAN_FILES "${CMAKE_CURRENT_SOURCE_DIR}/doc/html;${CMAKE_CURRENT_SOURCE_DIR}/doc/latex;" )
else( DOXYGEN_FOUND )
	message( STATUS "Doxygen not found - Documentation can't be generated." )
endif( DOXYGEN_FOUND )
